using System.Text.RegularExpressions;

public class EchoCommand : ICommand
{
    private const string QuotedPattern = @"(?<=')[^']*(?=')";
    private const string Pattern = @"\s+";

    public void Execute(string args)
    {
        if (string.IsNullOrWhiteSpace(args))
        {
            Console.WriteLine(string.Empty);
            return;
        }

        Console.WriteLine(!args.Contains('\'')
            ? Regex.Replace(args, Pattern, " ")
            : Regex.Matches(args, QuotedPattern)[0].Value);
    }
    
}