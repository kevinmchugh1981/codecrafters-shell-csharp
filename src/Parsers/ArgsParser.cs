public class ArgsParser : ParserBase, IParser
{
    public List<string> Parse(string args)
    {
        return string.IsNullOrEmpty(args) ? new List<string>() : GetBetweenDelimiters(args);
    }
    
    protected override Func< char, bool>? AdditionalProcessing => CheckArguments;

    private bool CheckArguments (char current)
    {
        if (EscapeNextChar || !char.IsWhiteSpace(current))
        {
            return false;
        }

        Result.Add(CurrentString);
        CurrentString = string.Empty;
        return true;
    }
}