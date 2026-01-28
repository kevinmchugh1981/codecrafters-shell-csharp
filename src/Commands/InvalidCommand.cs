public class InvalidCommand(string command, string arguments) : ICommand
{
    
    public string Arguments { get; } = arguments;

    private string Command { get; } = command;

    public void Execute()
    {
        var command = Command + (string.IsNullOrWhiteSpace(Arguments) ? string.Empty :$" {Arguments}");
        Console.Out.WriteLine($"{command}: command not found");
    }
}