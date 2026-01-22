using System.Text.RegularExpressions;

public class CatCommand :ICommand
{
    
    public void Execute(string args)
    {
        if (string.IsNullOrWhiteSpace(args))
        {
            Console.WriteLine(string.Empty);
            return;
        }

        var content = new List<string>();
        foreach (Match match in Regex.Matches(args, RegExs.QuotedPattern))
        {
            if(!File.Exists(match.Groups[0].Value))
                continue;
            using var stream = new StreamReader(match.Groups[0].Value);
            content.Add(stream.ReadToEnd().Trim());
        }
        
        Console.WriteLine(string.Join("", content));
    }

    
}