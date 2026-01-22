public static class CommandFactory
{
    public static ICommand CreateCommand(string command)
    {
        return command switch
        {
            "echo" => new EchoCommand(),
            "type" => new TypeCommand(),
            "pwd" => new PwdCommand(),
            "cd" => new ChangeDirectoryCommand(),
            _ => FileSearcher.IsExecutable(command, out var filePath)
                ? new ExternalCommand(filePath)
                : new InvalidCommand()
        };
    }
}