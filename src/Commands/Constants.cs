internal static class Constants
{
    internal static readonly Dictionary<string, Func<string, ICommand>> Commands =
        new()
        {
            { EchoTitle, (x) => new EchoCommand(x) },
            { TypeTitle, (x) => new TypeCommand(x) },
            { PwdTitle, (x) => new PwdCommand() },
            { ChangeDirectoryTitle, (x) => new ChangeDirectoryCommand(x) },
            { CatTitle, (x) => new CatCommand(x) },
            { ExitTitle, (x) => new ExitCommand(x) },
            {ListTitle, (x)=> new ListCommand(x) },
        };

    internal static bool IsShellBuiltIn(string command)
    {
        var shellBuiltInCommands = new[] { EchoTitle, TypeTitle, PwdTitle, ChangeDirectoryTitle, ExitTitle };
        return shellBuiltInCommands.Contains(command);
    }

    private const string EchoTitle = "echo";
    private const string TypeTitle = "type";
    private const string PwdTitle = "pwd";
    private const string ChangeDirectoryTitle = "cd";
    private const string CatTitle = "cat";
    private const string ExitTitle = "exit";
    private const string ListTitle = "ls";
    
    internal const string ListSwitch = "-1";
}