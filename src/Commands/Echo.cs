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

        Console.WriteLine(!args.Contains("'")
            ? Regex.Replace(args, RegExs.SpacePattern, " ")
            : string.Join("", Regex.Matches(args, RegExs.QuotedPattern)));
    }
    
}