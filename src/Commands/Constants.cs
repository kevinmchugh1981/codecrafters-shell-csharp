internal static class Constants
{
    internal static readonly Dictionary<string, Func<string,RedirectType, ICommand>> Commands =
        new()
        {
            { EchoTitle, (x, y) => new EchoCommand(x,y) },
            { TypeTitle, (x,y) => new TypeCommand(x) },
            { PwdTitle, (x,y) => new PwdCommand() },
            { ChangeDirectoryTitle, (x,y) => new ChangeDirectoryCommand(x) },
            { CatTitle, (x,y) => new CatCommand(x,y) },
            { ExitTitle, (x,y) => new ExitCommand(x) },
            {ListTitle, (x,y)=> new ListCommand(x,y) },
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