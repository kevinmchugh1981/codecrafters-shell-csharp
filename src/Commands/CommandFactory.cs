public static class CommandFactory
{
    public static ICommand CreateCommand(string command)
    {

        if (CommandsEnum.Commands.TryGetValue(command, out var newCommand))
            return newCommand();
        
        if (FileSearcher.IsExecutable(command, out var filePath))
            return new ExternalCommand(filePath);
        return new InvalidCommand(command);
    }
}