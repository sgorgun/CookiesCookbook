

using System.Text.Json;

namespace CookiesCookbook.DataAccess;

/// <summary>
/// read/write recepies from/to Json file.
/// </summary>
public class StringsJsonRepository : StringsRepository
{
    protected override string StringsToText(List<string> strings)
    {
        return JsonSerializer.Serialize(strings);
    }

    protected override List<string> TextToStrings(string fileContents)
    {
        return JsonSerializer.Deserialize<List<string>>(fileContents);
    }
}
