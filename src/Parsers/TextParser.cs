public class TextParser : ParserBase, IParser
{


    public List<string> Parse( string? str)
    {
        return string.IsNullOrWhiteSpace(str) ? new List<string>() : GetBetweenDelimiters(str);
    }
    
    protected override Func<char,bool>? AdditionalProcessing => ParseWhiteSpace;

    private bool ParseWhiteSpace(char current)
    {
        if (InsideDelimiter || EscapeNextChar || !char.IsWhiteSpace(current)
            || string.IsNullOrWhiteSpace(CurrentString))
        {
            return false;
        }

        Result.Add(CurrentString);
        CurrentString = string.Empty;
        return true;

    }
}