public class ExitCommand(string arguments) : BaseCommand(RedirectType.None, arguments)
{
    
    public override void Execute()
    {
        Environment.Exit(0);
    }
}