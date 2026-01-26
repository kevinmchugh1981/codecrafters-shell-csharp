public class ArgsParser : ParserBase, IParser
{
    public List<string> Parse(string args)
    {
        return string.IsNullOrEmpty(args) ? new List<string>() : GetBetweenDelimiters(args);
    }
    
    protected override Func< char, bool>? AdditionalProcessing => CheckArguments;

    private bool CheckArguments (char current)
    {

        if (InsideDelimiter)
            EscapeNextChar = false;
        
        if (EscapeNextChar || !char.IsWhiteSpace(current))
        {
            return false;
        }

        if(!string.IsNullOrWhiteSpace(CurrentString))
            Result.Add(CurrentString);
        CurrentString = string.Empty;
        return true;
    }
}