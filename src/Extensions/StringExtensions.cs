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
        var insideDelimiter = false;
        var currentString = string.Empty;
        var currentDelimiter = char.MinValue;

        for (var x = 0; x < str.Length; x++)
        {
            //Inside a delimiter and char doesn't close it.
            if (insideDelimiter && str[x] != currentDelimiter)
                currentString += str[x];
            //Not inside a delimiter and the char opens delimiter.
            else if (!insideDelimiter && Delimiters.Contains(str[x]) && currentDelimiter != str[x])
            {
                //Store current string before, opening new one.
                if (!string.IsNullOrWhiteSpace(currentString) && x - 1 >= 0 && !Delimiters.Contains(str[x - 1]) && 
                    (x +1 <= str.Length - 1 && !Delimiters.Contains(str[x + 1])))
                {
                    result.Add(currentString);
                    currentString = string.Empty;
                }
                currentDelimiter = str[x];
                insideDelimiter = true;
                continue;
            }
            //Inside a delimiter and char closes delimiter
            else if (insideDelimiter && Delimiters.Contains(str[x]) && currentDelimiter == str[x])
            {
                //Store current string before closing.
                if (!string.IsNullOrWhiteSpace(currentString) 
                    && ((x +1 <= str.Length - 1 && !Delimiters.Contains(str[x + 1]) && str[x]!=str[x-1])|| x== str.Length-1) )
                {
                    result.Add(currentString);
                    currentString = string.Empty;
                }
                currentDelimiter = char.MinValue;
                insideDelimiter = false;
                continue;
            }
            //If you aren't inside a delimiter and this isn't one, add none-whitespace chars
            else if (!insideDelimiter && !Delimiters.Contains(str[x]))
                currentString += char.IsWhiteSpace(str[x]) ? string.Empty : str[x];

            //If this is the end then store remaining string.
            if (!string.IsNullOrWhiteSpace(currentString) && x == str.Length - 1)
                result.Add(currentString);
        }

        return result;
    }
}