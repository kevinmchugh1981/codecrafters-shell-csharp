public static class CommandFactory
{
    public static ICommand CreateCommand(string command)
    {

        if (CommandsEnum.Commands.Contains(command))
        {
            return command switch
            {
                "echo" => new EchoCommand(),
                "type" => new TypeCommand(),
                "pwd" => new PwdCommand(),
                "cat" => new CatCommand(),
                "cd" => new ChangeDirectoryCommand(),
                _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
            };
        }

        if (FileSearcher.IsExecutable(command, out var filePath))
            return new ExternalCommand(filePath);
        return new InvalidCommand(command);
    }
}