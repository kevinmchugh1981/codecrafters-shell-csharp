public class ExitCommand(string arguments) : ICommand
{
    public string Arguments { get; } = arguments;

    public void Execute()
    {
        System.Environment.Exit(0);
    }
}