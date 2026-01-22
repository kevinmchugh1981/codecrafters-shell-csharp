
internal static class CommandsEnum
{
    internal static readonly Dictionary<string, Func< ICommand>> Commands =
        new()
        {
            { "echo", () => new EchoCommand() },
            { "type", () => new TypeCommand() },
            { "pwd", () => new PwdCommand() },
            { "cd", () => new ChangeDirectoryCommand() },
            { "cat", () => new CatCommand() },
        };
}