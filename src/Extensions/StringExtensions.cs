internal static class StringExtensions
{
    private static readonly List<char> Delimiters = ['\'', '"'];

    internal static List<string> Parse(this string args)
    {
        var result = new List<string>();

        if (string.IsNullOrWhiteSpace(args))
            return result;

        //Only blank spaces.
        if (args.Contains(Delimiters[0]) || args.Contains(Delimiters[1]))
        {
            return GetBetweenDelimiters(args);
        }

        result = args.Split(" ").ToList().Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
        return result;
    }

    private static List<string> GetBetweenDelimiters(string str)
    {
        var result = new List<string>();
        var insideQuotes = false;
        var currentString = string.Empty;
        var doubleDelimiter = false;
        var currentDelimiter = char.MinValue;
        for (var x = 0; x < str.Length; x++)
        {
            if (Delimiters.Contains(str[x]))
            {
                if (!insideQuotes)
                    currentDelimiter = str[x];

                insideQuotes = !insideQuotes;
                if (!insideQuotes)
                {
                    if (currentString != string.Empty)
                        if (!doubleDelimiter)
                        {
                            result.Add(currentString);
                            currentString = string.Empty;
                        }
                        else
                            doubleDelimiter = false;
                }
                else if (x == currentDelimiter && x + 1 <= str.Length - 1)
                {
                    doubleDelimiter = str[x] == str[x + 1];
                }
            }
            else if (insideQuotes)
            {
                currentString += str[x];
            }
            else if (!insideQuotes)
            {
                if (char.IsWhiteSpace(str[x]) && !string.IsNullOrWhiteSpace(currentString))
                {
                    result.Add(currentString);
                    currentString = string.Empty;
                }
                else if (!char.IsWhiteSpace(str[x]))
                {
                    currentString += str[x];
                }
            }

            if (x == str.Length - 1 && !string.IsNullOrWhiteSpace(currentString))
                result.Add(currentString);
        }

        return result;
    }
}