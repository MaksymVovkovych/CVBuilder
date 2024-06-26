﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using CVBuilder.Application.Caching;
using CVBuilder.Application.Core.Infrastructure.Interfaces;

namespace CVBuilder.Application.Helpers;

/// <summary>
///     Represents a common helper
/// </summary>
public class CommonHelper
{
    public static ICVBuilderFileProvider DefaultFileProvider { get; set; }

    /// <summary>
    ///     Ensures the subscriber email or throw.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <returns></returns>
    public static string EnsureSubscriberEmailOrThrow(string email)
    {
        var output = EnsureNotNull(email);
        output = output.Trim();
        output = EnsureMaximumLength(output, 255);

        if (!IsValidEmail(output)) throw new Exception("Email is not valid.");

        return output;
    }

    /// <summary>
    ///     Verifies that a string is in valid e-mail format
    /// </summary>
    /// <param name="email">Email to verify</param>
    /// <returns>true if the string is a valid e-mail address and false if it's not</returns>
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        email = email.Trim();
        var result = Regex.IsMatch(email,
            "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$",
            RegexOptions.IgnoreCase);
        return result;
    }

    /// <summary>
    ///     Verifies that string is an valid IP-Address
    /// </summary>
    /// <param name="ipAddress">IPAddress to verify</param>
    /// <returns>true if the string is a valid IpAddress and false if it's not</returns>
    public static bool IsValidIpAddress(string ipAddress)
    {
        IPAddress ip;
        return IPAddress.TryParse(ipAddress, out ip);
    }

    /// <summary>
    ///     Generate random digit code
    /// </summary>
    /// <param name="length">Length</param>
    /// <returns>Result string</returns>
    public static string GenerateRandomDigitCode(int length)
    {
        var random = new Random();
        var str = string.Empty;
        for (var i = 0; i < length; i++)
            str = string.Concat(str, random.Next(10).ToString());
        return str;
    }

    /// <summary>
    ///     Ensure that a string doesn't exceed maximum allowed length
    /// </summary>
    /// <param name="str">Input string</param>
    /// <param name="maxLength">Maximum length</param>
    /// <param name="postfix">A string to add to the end if the original string was shorten</param>
    /// <returns>Input string if its lengh is OK; otherwise, truncated input string</returns>
    public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        if (str.Length > maxLength)
        {
            var pLen = postfix?.Length ?? 0;

            var result = str.Substring(0, maxLength - pLen);
            if (!string.IsNullOrEmpty(postfix)) result += postfix;
            return result;
        }

        return str;
    }

    /// <summary>
    ///     Ensures that a string only contains numeric values
    /// </summary>
    /// <param name="str">Input string</param>
    /// <returns>Input string with only numeric values, empty string if input is null/empty</returns>
    public static string EnsureNumericOnly(string str)
    {
        return string.IsNullOrEmpty(str) ? string.Empty : new string(str.Where(p => char.IsDigit(p)).ToArray());
    }

    /// <summary>
    ///     Ensure that a string is not null
    /// </summary>
    /// <param name="str">Input string</param>
    /// <returns>Result</returns>
    public static string EnsureNotNull(string str)
    {
        return str ?? string.Empty;
    }

    /// <summary>
    ///     Indicates whether the specified strings are null or empty strings
    /// </summary>
    /// <param name="stringsToValidate">Array of strings to validate</param>
    /// <returns>Boolean</returns>
    public static bool AreNullOrEmpty(params string[] stringsToValidate)
    {
        return stringsToValidate.Any(p => string.IsNullOrEmpty(p));
    }

    /// <summary>
    ///     Compare two arrasy
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="a1">Array 1</param>
    /// <param name="a2">Array 2</param>
    /// <returns>Result</returns>
    public static bool ArraysEqual<T>(T[] a1, T[] a2)
    {
        //also see Enumerable.SequenceEqual(a1, a2);
        if (ReferenceEquals(a1, a2))
            return true;

        if (a1 == null || a2 == null)
            return false;

        if (a1.Length != a2.Length)
            return false;

        var comparer = EqualityComparer<T>.Default;
        for (var i = 0; i < a1.Length; i++)
            if (!comparer.Equals(a1[i], a2[i]))
                return false;
        return true;
    }

    /// <summary>
    ///     Sets a property on an object to a valuae.
    /// </summary>
    /// <param name="instance">The object whose property to set.</param>
    /// <param name="propertyName">The name of the property to set.</param>
    /// <param name="value">The value to set the property to.</param>
    public static void SetProperty(object instance, string propertyName, object value)
    {
        if (instance == null) throw new ArgumentNullException("instance");
        if (propertyName == null) throw new ArgumentNullException("propertyName");

        var instanceType = instance.GetType();
        var pi = instanceType.GetProperty(propertyName);
        if (pi == null)
            throw new Exception(string.Format("No property '{0}' found on the instance of type '{1}'.",
                propertyName, instanceType));
        if (!pi.CanWrite)
            throw new Exception(string.Format(
                "The property '{0}' on the instance of type '{1}' does not have a setter.", propertyName,
                instanceType));
        if (value != null && !value.GetType().IsAssignableFrom(pi.PropertyType))
            value = To(value, pi.PropertyType);
        pi.SetValue(instance, value, new object[0]);
    }

    /// <summary>
    ///     Converts a value to a destination type.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="destinationType">The type to convert the value to.</param>
    /// <returns>The converted value.</returns>
    public static object To(object value, Type destinationType)
    {
        return To(value, destinationType, CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///     Converts a value to a destination type.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="destinationType">The type to convert the value to.</param>
    /// <param name="culture">Culture</param>
    /// <returns>The converted value.</returns>
    public static object To(object value, Type destinationType, CultureInfo culture)
    {
        if (value != null)
        {
            var sourceType = value.GetType();

            var destinationConverter = TypeDescriptor.GetConverter(destinationType);
            if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                return destinationConverter.ConvertFrom(null, culture, value);

            var sourceConverter = TypeDescriptor.GetConverter(sourceType);
            if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                return sourceConverter.ConvertTo(null, culture, value, destinationType);

            if (destinationType.IsEnum && value is int)
                return Enum.ToObject(destinationType, (int) value);

            if (!destinationType.IsInstanceOfType(value))
                return Convert.ChangeType(value, destinationType, culture);
        }

        return value;
    }

    /// <summary>
    ///     Converts a value to a destination type.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <typeparam name="T">The type to convert the value to.</typeparam>
    /// <returns>The converted value.</returns>
    public static T To<T>(object value)
    {
        //return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        return (T) To(value, typeof(T));
    }

    /// <summary>
    ///     Convert enum for front-end
    /// </summary>
    /// <param name="str">Input string</param>
    /// <returns>Converted string</returns>
    public static string ConvertEnum(string str)
    {
        if (string.IsNullOrEmpty(str)) return string.Empty;
        var result = string.Empty;
        foreach (var c in str)
            if (c.ToString() != c.ToString().ToLower())
                result += " " + c;
            else
                result += c.ToString();

        //ensure no spaces (e.g. when the first letter is upper case)
        result = result.TrimStart();
        return result;
    }

    public static string GetEntityCacheKey(Type entityType, object id)
    {
        return string.Format(CachingDefaults.EntityCacheKey, entityType.Name.ToLower(), id);
    }

    public static string GetEntityNameKey(string name)
    {
        return name.EndsWith("y")
            ? $"{name.Remove(name.Length - 1, 1)}ies"
            : $"{name}s";
    }
}