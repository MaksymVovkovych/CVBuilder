using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CVBuilder.EFContext.Extensions;

public static class EnumExtensions
{
    /// <summary>
    ///     Returns description of enum item marked with "Description" attribute
    /// </summary>
    public static string ToDescription(this Enum en) //ext method
    {
        var descriptionAttribute = GetAttributeOrDefault<DescriptionAttribute>(en);
        if (descriptionAttribute != null) return descriptionAttribute.Description;

        var displayNameAttribute = GetAttributeOrDefault<DisplayNameAttribute>(en);
        if (displayNameAttribute != null) return displayNameAttribute.DisplayName;

        return en.ToString();
    }

    public static T GetAttributeOrDefault<T>(this Enum enumeration)
        where T : Attribute
    {
        return GetAttributeInner<T>(enumeration);
    }

    public static List<KeyValuePair<T, string>> EnumToList<T>()
    {
        var enumType = typeof(T);
        var enumValArray = Enum.GetValues(enumType);
        var enumValList = new List<KeyValuePair<T, string>>(enumValArray.Length);
        foreach (int val in enumValArray)
        {
            var item = Enum.Parse(enumType, val.ToString());
            enumValList.Add(new KeyValuePair<T, string>((T)item, ((Enum)item).ToDescription()));
        }

        return enumValList;
    }

    private static T GetAttributeInner<T>(Enum enumeration) where T : Attribute
    {
        var members = enumeration
            .GetType()
            .GetMember(enumeration.ToString()).ToList();
        if (members.Count <= 0)
            throw new Exception($"Value {enumeration} does not belong to enumeration {enumeration.GetType()}");

        return members[0]
            .GetCustomAttributes(typeof(T), false)
            .Cast<T>()
            .SingleOrDefault();
    }
}