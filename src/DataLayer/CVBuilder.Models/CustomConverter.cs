using System.Globalization;

namespace CVBuilder.Models;

public static class CustomConverter
{
    public static decimal? ToDecimal(string str)
    {
        var success = decimal.TryParse(str, NumberStyles.Currency, CultureInfo.InvariantCulture, out var number);
        if (success)
            return number;
        return null;
    }
}