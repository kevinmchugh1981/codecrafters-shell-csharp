using System.Text.RegularExpressions;

public class CatCommand :ICommand
{
    private const string QuotedPattern = "'([^']*)'";
    
    public void Execute(string args)
    {
        if (string.IsNullOrWhiteSpace(args))
        {
            Console.WriteLine(string.Empty);
            return;
        }

        var content = new List<string>();
        foreach (Match match in Regex.Matches(args, QuotedPattern))
        {
            if(!File.Exists(match.Groups[1].Value))
                continue;
            using var stream = new StreamReader(match.Groups[1].Value);
            content.Add(stream.ReadToEnd());
        }
        
        Console.WriteLine(string.Join(" ", content));
    }

    
}