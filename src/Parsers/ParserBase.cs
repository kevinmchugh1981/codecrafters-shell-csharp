public abstract class ParserBase
{
    private static readonly List<char> Delimiters = new() { '\'', '"' };

    protected abstract Func< char,bool>? AdditionalProcessing { get; }

    protected List<string> Result = new();
    protected string CurrentString = string.Empty;
    protected bool EscapeNextChar;
    protected bool InsideDelimiter;
    private char currentDelimiter = char.MinValue;

    protected List<string> GetBetweenDelimiters(string str)
    {
       Result = new List<string>();
        CurrentString = string.Empty;
        EscapeNextChar = false;
        InsideDelimiter = false;
        currentDelimiter = char.MinValue;

        for (var x = 0; x < str.Length; x++)
        {
            //Outside a delimiter and char is an escape char, which you aren't already escaping
            if (!InsideDelimiter && str[x] == '\\' && !EscapeNextChar)
            {
                EscapeNextChar = true;
                continue;
            }

            //Inside a delimiter and char doesn't close it.
            if (InsideDelimiter && str[x] != currentDelimiter)
                CurrentString += str[x];
            //Not inside a delimiter and the char opens delimiter.
            else if (!InsideDelimiter && Delimiters.Contains(str[x]) && currentDelimiter != str[x] && !EscapeNextChar)
            {
                //Store current string before, opening new one.
                if (!string.IsNullOrWhiteSpace(CurrentString) && currentDelimiter != char.MinValue && x - 1 >= 0 && !Delimiters.Contains(str[x - 1]) &&
                    (x + 1 <= str.Length - 1 && !Delimiters.Contains(str[x + 1])))
                {
                    Result.Add(CurrentString);
                    CurrentString = string.Empty;
                }

                currentDelimiter = str[x];
                InsideDelimiter = true;
                continue;
            }
            //Inside a delimiter and char closes delimiter
            else if (InsideDelimiter && Delimiters.Contains(str[x]) && currentDelimiter == str[x])
            {
                //Store current string before closing.
                if (!string.IsNullOrWhiteSpace(CurrentString)
                    && ((x + 1 <= str.Length - 1 && !Delimiters.Contains(str[x + 1]) && str[x] != str[x - 1]) ||
                        x == str.Length - 1))
                {
                    Result.Add(CurrentString);
                    CurrentString = string.Empty;
                }

                currentDelimiter = char.MinValue;
                InsideDelimiter = false;
                continue;
            }
            //If you aren't inside a delimiter and this isn't one, add none-whitespace chars
            else if (!InsideDelimiter && (!Delimiters.Contains(str[x]) || EscapeNextChar))
            {
                if (CurrentString.Count(y => y == '"') % 2 != 0)
                {
                    CurrentString += str[x];
                }
                else if (AdditionalProcessing != null && AdditionalProcessing(str[x]))
                {
                    continue;
                }
                else{
                    CurrentString += char.IsWhiteSpace(str[x]) && !EscapeNextChar ? string.Empty : str[x];
                }


                if (EscapeNextChar) EscapeNextChar = false;
            }


            //If this is the end then store remaining string.
            if (!string.IsNullOrWhiteSpace(CurrentString) && x == str.Length - 1)
                Result.Add(CurrentString);
        }

        return Result;
    }
}