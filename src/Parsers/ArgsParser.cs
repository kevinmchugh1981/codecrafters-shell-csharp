public class ArgsParser : ParserBase, IParser
{
    public List<string> Parse(string args)
    {
        var test = GetBetweenDelimiters(args);
        test = test.Select(x=> x.Replace(" ", string.Empty)).ToList();
        return test;
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