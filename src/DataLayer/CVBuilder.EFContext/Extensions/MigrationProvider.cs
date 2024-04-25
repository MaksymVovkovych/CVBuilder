using Microsoft.EntityFrameworkCore.DataEncryption;

namespace CVBuilder.EFContext.Extensions;

public class MigrationProvider: IEncryptionProvider
{
    private readonly IEncryptionProvider _encryption;

    public MigrationProvider(IEncryptionProvider encryption)
    {
        _encryption = encryption;
    }

    public byte[] Encrypt(byte[] input)
    {
        return _encryption.Encrypt(input);
    }

    public byte[] Decrypt(byte[] input)
    {
        return input;
    }
}