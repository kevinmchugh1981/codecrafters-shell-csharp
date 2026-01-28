internal static class Constants
{
    internal static readonly Dictionary<string, Func<ICommand>> Commands =
        new()
        {
            { EchoTitle, () => new EchoCommand() },
            { TypeTitle, () => new TypeCommand() },
            { PwdTitle, () => new PwdCommand() },
            { ChangeDirectoryTitle, () => new ChangeDirectoryCommand() },
            { CatTitle, () => new CatCommand() },
            { ExitTitle, () => new ExitCommand() }
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
}