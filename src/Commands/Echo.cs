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
            Console.WriteLine(string.Join("", Regex.Matches(args, RegExs.QuotedPattern)));
        else if (args.Contains('"'))
            Console.WriteLine(string.Join("", Regex.Matches(args, RegExs.DoubleQuotedPattern)));
        else
            Console.WriteLine(Regex.Replace(args, RegExs.SpacePattern, " "));
    }
}