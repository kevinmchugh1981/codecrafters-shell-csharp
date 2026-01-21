public static class CommandFactory
{
    public static ICommand CreateCommand(string command)
    {
        return command switch
        {
            "echo" => new EchoCommand(),
            "type" => new TypeCommand(),
            _ => new ExternalCommand()
        };
    }
}