namespace CookiesCookbook.FileAccess;

/// <summary>
/// Build a file path from the file's name and the file gotmat enum.
/// </summary>
public class FileMetadata
{
    public string Name { get; set; }

    public FileFormat Format { get; }

    public FileMetadata(string name, FileFormat format)
    {
        Name = name;
        Format = format;
    }

    public string ToPath() => $"{Name}.{Format.AsFileExtension()}";
}
