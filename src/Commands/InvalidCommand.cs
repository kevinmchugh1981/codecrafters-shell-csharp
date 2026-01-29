public class InvalidCommand(string command, string arguments) : BaseCommand
{
    
    public override string Arguments { get; } = arguments;
    public override bool CanRedirect => false;

    private string Command { get; } = command;

    public override void Execute()
    {
        var command = Command + (string.IsNullOrWhiteSpace(Arguments) ? string.Empty :$" {Arguments}");
        Output($"{command}: command not found");
    }
}