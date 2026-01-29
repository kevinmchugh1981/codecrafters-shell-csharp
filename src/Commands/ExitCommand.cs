public class ExitCommand(string arguments) : BaseCommand
{
    public override string Arguments { get; } = arguments;
    public override bool CanRedirect => false;

    public override void Execute()
    {
        Environment.Exit(0);
    }
}