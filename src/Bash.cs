

using System.Text.RegularExpressions;

internal class Bash
{
    private readonly string pattern = @"^(?<command>\S+)(?:\s+(?<arguments>.*))?$";

    public void Start()
    {
        for ((string,string) input; (input = GetInput()).Item1 != "exit";)
        {
            
            if (string.IsNullOrWhiteSpace(input.Item1))
                continue;

            var command = CommandFactory.CreateCommand(input.Item1);
            command.Execute(input.Item2);
        }
    }
    
    private (string,string) GetInput()
    {
        Console.Write("$ ");
        return ParseCommand(Console.ReadLine());
    }

    private (string, string) ParseCommand(string? input)
    {
        if(string.IsNullOrWhiteSpace(input))
            return (string.Empty, string.Empty);
        
        var match = Regex.Match(input, pattern);
        return match.Success ? (match.Groups["command"].Value, match.Groups["arguments"].Value) : (string.Empty, string.Empty);
    }

}