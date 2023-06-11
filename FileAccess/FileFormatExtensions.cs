namespace CookiesCookbook.FileAccess;

/// <summary>
/// File format extension.
/// </summary>
public static class FileFormatExtensions
{
    public static string AsFileExtension(this FileFormat fileFormat) =>
        fileFormat == FileFormat.Json ? "json" : "txt";
}
