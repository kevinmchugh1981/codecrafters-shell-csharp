internal static class StringExtensions
{
    private static readonly List<char> Delimiters = ['\'', '"'];

    internal static List<string> Parse(this string str)
    {
        var result = new List<string>();

        return string.IsNullOrWhiteSpace(str) ? result : GetBetweenDelimiters(str);
    }

    private static List<string> GetBetweenDelimiters(string str)
    {
        var result = new List<string>();
                var insideDelimiter = false;
                var currentString = string.Empty;
                var currentDelimiter = char.MinValue;
                var escapeNextChar = false;
        
                for (var x = 0; x < str.Length; x++)
                {
                    //Outside a delimiter and char is an escape char, which you aren't already escaping
                    if (!insideDelimiter && str[x] == '\\' && !escapeNextChar)
                    {
                        escapeNextChar = true;
                        continue;
                    }
        
                    //Inside a delimiter and char doesn't close it.
                    if (insideDelimiter && str[x] != currentDelimiter)
                        currentString += str[x];
                    //Not inside a delimiter and the char opens delimiter.
                    else if (!insideDelimiter && Delimiters.Contains(str[x]) && currentDelimiter != str[x] && !escapeNextChar)
                    {
                        //Store current string before, opening new one.
                        if (!string.IsNullOrWhiteSpace(currentString) && x - 1 >= 0 && !Delimiters.Contains(str[x - 1]) &&
                            (x + 1 <= str.Length - 1 && !Delimiters.Contains(str[x + 1])))
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
                            && ((x + 1 <= str.Length - 1 && !Delimiters.Contains(str[x + 1]) && str[x] != str[x - 1]) ||
                                x == str.Length - 1))
                        {
                            result.Add(currentString);
                            currentString = string.Empty;
                        }
        
                        currentDelimiter = char.MinValue;
                        insideDelimiter = false;
                        continue;
                    }
                    //If you aren't inside a delimiter and this isn't one, add none-whitespace chars
                    else if (!insideDelimiter && (!Delimiters.Contains(str[x]) || escapeNextChar))
                    {
                        if (currentString.Count(y => y == '"') % 2 != 0)
                        {
                            currentString += str[x];
                        }
                        else
                        {
                            currentString += char.IsWhiteSpace(str[x]) && !escapeNextChar ? string.Empty : str[x];
                        }
                        
                        
                        if (escapeNextChar) escapeNextChar = false;
                    }
        
        
                    //If this is the end then store remaining string.
                    if (!string.IsNullOrWhiteSpace(currentString) && x == str.Length - 1)
                        result.Add(currentString);
                }
        
                return result;
    }
}