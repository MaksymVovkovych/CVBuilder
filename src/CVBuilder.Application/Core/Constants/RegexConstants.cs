namespace CVBuilder.Application.Core.Constants;

public static class RegexConstants
{
    public const string PHONE_ALL_REGEX = "^$|[0-9 ()+-]+";
    public const string SITE_REGEX = "^$|(https?://)?([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?";
}