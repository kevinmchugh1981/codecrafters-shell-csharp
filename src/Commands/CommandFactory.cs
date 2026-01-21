public static class CommandFactory
{
    public static ICommand CreateCommand(string command)
    {
        switch (command)
        {
            case "echo":
                return new EchoCommand();
            case "type":
                return new TypeCommand();
            default:
               return FileSearcher.IsExecutable(command, out var filePath) ? 
                     new ExternalCommand(filePath) : new InvalidCommand();
                
        }
    }
}