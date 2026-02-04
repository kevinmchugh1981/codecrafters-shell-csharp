public class InvalidCommand(string command, string arguments) : BaseCommand(RedirectType.None, arguments)
{
    
    private string Command { get; } = command;

    public override void Execute()
    {
        var command = Command + (string.IsNullOrWhiteSpace(Arguments) ? string.Empty :$" {Arguments}");
         Output($"{command}: command not found", true);
    }
}