public class TextParser : ParserBase, IParser
{


    public List<string> Parse( string? str)
    {
        return string.IsNullOrWhiteSpace(str) ? new List<string>() : GetBetweenDelimiters(str);
    }
    
    protected override Func<char,bool>? AdditionalProcessing => null;
}