using System.Text.RegularExpressions;

public static class CommandFactory
{
    
    private static readonly string Pattern = @"^(?<command>\S+)(?:\s+(?<arguments>.*))?$";
    
    public static ICommand CreateCommand(string command)
    {

        var parseCommand = TryParseCommand(command);
        
        if (Constants.Commands.TryGetValue(parseCommand.Item1.ToLower(), out var newCommand))
            return newCommand(parseCommand.Item2, RedirectFunctions.GetRedirectType(parseCommand.Item2));
        
        if (FileSearcher.IsExecutable(command, out var filePath, out var parameters))
            return new ExternalCommand(filePath, parameters);
        
        
        return new InvalidCommand(parseCommand.Item1,  parseCommand.Item2);
    }

    private static (string, string) TryParseCommand(string input)
    {
        var match = Regex.Match(input, Pattern);
        return match.Success ? (match.Groups["command"].Value, match.Groups["arguments"].Value) : (string.Empty, string.Empty);
    }
}