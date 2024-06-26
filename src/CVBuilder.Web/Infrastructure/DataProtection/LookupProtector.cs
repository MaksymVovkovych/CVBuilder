﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;

namespace CVBuilder.Web.Infrastructure.DataProtection;

public class LookupProtector : ILookupProtector
{
    private readonly ProtectorAlgorithm _defaultAlgorithm = ProtectorAlgorithm.Aes256Hmac512;
    private readonly ILookupProtectorKeyRing _keyRing;

    public LookupProtector(ILookupProtectorKeyRing keyRing)
    {
        _keyRing = keyRing;
    }

    public string Protect(string keyId, string data)
    {
        // Get the default algorithms.
        // We does this so we can embed the algorithm details used in the cipher text so we can
        // change algorithms as yet another collision appears in a hashing algorithm.
        // See https://media.blackhat.com/bh-us-10/whitepapers/Sullivan/BlackHat-USA-2010-Sullivan-Cryptographic-Agility-wp.pdf
        ProtectorAlgorithmHelper.GetAlgorithms(
            _defaultAlgorithm,
            out var encryptingAlgorithm,
            out var signingAlgorithm,
            out var keyDerivationIterationCount);

        var masterKey = MasterKey(keyId);

        // Convert the string to bytes, because encryption works on bytes, not strings.
        var plainText = Encoding.UTF8.GetBytes(data);
        byte[] cipherTextAndIV;

        // Derive a key for encryption from the master key
        encryptingAlgorithm.Key = DerivedEncryptionKey(
            masterKey,
            encryptingAlgorithm,
            keyDerivationIterationCount);

        // As we need this to be deterministic, we need to force an IV that is derived from the plain text.
        encryptingAlgorithm.IV = DerivedInitializationVector(
            masterKey,
            data,
            encryptingAlgorithm,
            keyDerivationIterationCount);

        // And encrypt
        using (var ms = new MemoryStream())
        using (var cs = new CryptoStream(
                   ms,
                   encryptingAlgorithm.CreateEncryptor(),
                   CryptoStreamMode.Write))
        {
            cs.Write(plainText);
            cs.FlushFinalBlock();
            var encryptedData = ms.ToArray();

            cipherTextAndIV = CombineByteArrays(encryptingAlgorithm.IV, encryptedData);
        }

        // Now get a signature for the data so we can detect tampering in situ.
        var signature = SignData(
            cipherTextAndIV,
            masterKey,
            encryptingAlgorithm,
            signingAlgorithm,
            keyDerivationIterationCount);

        // Add the signature to the cipher text.
        var signedData = CombineByteArrays(signature, cipherTextAndIV);

        // Add our algorithm identifier to the combined signature and cipher text.
        var algorithmIdentifier = BitConverter.GetBytes((int) _defaultAlgorithm);
        var output = CombineByteArrays(algorithmIdentifier, signedData);

        // Clean everything up.
        encryptingAlgorithm.Clear();
        signingAlgorithm.Clear();
        encryptingAlgorithm.Dispose();
        signingAlgorithm.Dispose();

        Array.Clear(plainText, 0, plainText.Length);

        // Return the results as a string.
        return Convert.ToBase64String(output);
    }

    public string Unprotect(string keyId, string data)
    {
        var masterKey = MasterKey(keyId);
        byte[] plainText;

        // Take our string and convert it back to bytes.
        var payload = Convert.FromBase64String(data);

        // Read the saved algorithm details and create instances of those algorithms.
        var algorithmIdentifierAsBytes = new byte[4];
        Buffer.BlockCopy(payload, 0, algorithmIdentifierAsBytes, 0, 4);
        var algorithmIdentifier = (ProtectorAlgorithm) BitConverter.ToInt32(algorithmIdentifierAsBytes, 0);
        ProtectorAlgorithmHelper.GetAlgorithms(
            _defaultAlgorithm,
            out var encryptingAlgorithm,
            out var signingAlgorithm,
            out var keyDerivationIterationCount);

        // Now extract the signature
        var signature = new byte[signingAlgorithm.HashSize / 8];
        Buffer.BlockCopy(payload, 4, signature, 0, signingAlgorithm.HashSize / 8);

        // And finally grab the rest of the data
        var dataLength = payload.Length - 4 - signature.Length;
        var cipherTextAndIV = new byte[dataLength];
        Buffer.BlockCopy(payload, 4 + signature.Length, cipherTextAndIV, 0, dataLength);

        // Check the signature before anything else is done to detect tampering and avoid
        // oracles.
        var computedSignature = SignData(
            cipherTextAndIV,
            masterKey,
            encryptingAlgorithm,
            signingAlgorithm,
            keyDerivationIterationCount);
        if (!ByteArraysEqual(computedSignature, signature)) throw new CryptographicException(@"Invalid Signature.");

        signingAlgorithm.Clear();
        signingAlgorithm.Dispose();

        // The signature is valid, so now we can work on decrypting the data.
        var ivLength = encryptingAlgorithm.BlockSize / 8;
        var initializationVector = new byte[ivLength];
        var cipherText = new byte[cipherTextAndIV.Length - ivLength];
        // The IV is embedded in the cipher text, so we extract it out.
        Buffer.BlockCopy(cipherTextAndIV, 0, initializationVector, 0, ivLength);
        // Then we get the encrypted data.
        Buffer.BlockCopy(cipherTextAndIV, ivLength, cipherText, 0, cipherTextAndIV.Length - ivLength);

        encryptingAlgorithm.Key = DerivedEncryptionKey(
            masterKey,
            encryptingAlgorithm,
            keyDerivationIterationCount);
        encryptingAlgorithm.IV = initializationVector;

        // Decrypt
        using (var ms = new MemoryStream())
        using (var cs = new CryptoStream(ms, encryptingAlgorithm.CreateDecryptor(), CryptoStreamMode.Write))
        {
            cs.Write(cipherText);
            cs.FlushFinalBlock();
            plainText = ms.ToArray();
        }

        encryptingAlgorithm.Clear();
        encryptingAlgorithm.Dispose();

        // And convert from the bytes back to a string.
        return Encoding.UTF8.GetString(plainText);
    }

    public byte[] MasterKey(string keyId)
    {
        return Convert.FromBase64String(_keyRing[keyId]);
    }

    private static byte[] SignData(byte[] cipherText, byte[] masterKey, SymmetricAlgorithm symmetricAlgorithm,
        KeyedHashAlgorithm hashAlgorithm, int keyDerivationIterationCount)
    {
        hashAlgorithm.Key = DerivedSigningKey(masterKey, symmetricAlgorithm, keyDerivationIterationCount);
        var signature = hashAlgorithm.ComputeHash(cipherText);
        hashAlgorithm.Clear();
        return signature;
    }

    private static byte[] DerivedSigningKey(byte[] key, SymmetricAlgorithm algorithm,
        int keyDerivationIterationCount)
    {
        return KeyDerivation.Pbkdf2(
            @"IdentityLookupDataSigning",
            key,
            KeyDerivationPrf.HMACSHA512,
            keyDerivationIterationCount,
            algorithm.KeySize / 8);
    }

    private static byte[] DerivedEncryptionKey(byte[] key, SymmetricAlgorithm algorithm,
        int keyDerivationIterationCount)
    {
        return KeyDerivation.Pbkdf2(
            @"IdentityLookupEncryption",
            key,
            KeyDerivationPrf.HMACSHA512,
            keyDerivationIterationCount,
            algorithm.KeySize / 8);
    }

    private static byte[] DerivedInitializationVector(byte[] key, string plainText, SymmetricAlgorithm algorithm,
        int keyDerivationIterationCount)
    {
        return KeyDerivation.Pbkdf2(
            plainText,
            key,
            KeyDerivationPrf.HMACSHA512,
            keyDerivationIterationCount,
            algorithm.BlockSize / 8);
    }

    private static byte[] CombineByteArrays(byte[] left, byte[] right)
    {
        var output = new byte[left.Length + right.Length];
        Buffer.BlockCopy(left, 0, output, 0, left.Length);
        Buffer.BlockCopy(right, 0, output, left.Length, right.Length);

        return output;
    }

    [MethodImpl(MethodImplOptions.NoOptimization)]
    private static bool ByteArraysEqual(IReadOnlyList<byte> a, IReadOnlyList<byte> b)
    {
        if (ReferenceEquals(a, b)) return true;

        if (a == null || b == null || a.Count != b.Count) return false;

        var areSame = true;
        for (var i = 0; i < a.Count; i++) areSame &= a[i] == b[i];

        return areSame;
    }
}