using System.Text.RegularExpressions;

public class EchoCommand : ICommand
{
    public void Execute(string args)
    {
        if (string.IsNullOrWhiteSpace(args))
        {
            Console.WriteLine(string.Empty);
            return;
        }

        if (args.Contains('\''))
            Console.WriteLine(string.Join(" ", Extract(args, RegExs.QuotedPattern,1)));
        else if (args.Contains('"'))
            Console.WriteLine(string.Join(" ", Extract(args, RegExs.DoubleQuotedPattern,1)));
        else
            Console.WriteLine(Regex.Replace(args, RegExs.SpacePattern, " "));
    }

    private List<string> Extract(string input, string pattern, int group)
    {
        return Regex.Matches(input, pattern)
            .Select(m => m.Groups[group].Value) 
            .ToList();
    }
}