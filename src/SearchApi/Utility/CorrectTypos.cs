public class CorrectTypos
{
    static Dictionary<string, string> typoDictionary = new Dictionary<string, string>
    {
        {"Self-help", "Selfhelp"},
        {"Sci-fi", "Scifi"},
        // Add more common typos and their corrections as needed
    };
    // we can use nuget pachake like NHunspell for this. But for demo I created a dictonary only
    public static string CorrectTypo(string input)
    {
        foreach (var typo in typoDictionary)
        {
            input = input.Replace(typo.Key, typo.Value);
        }
        return input;
    }
}
