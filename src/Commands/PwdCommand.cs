public class PwdCommand() : BaseCommand(RedirectType.None, string.Empty)
{
    
    public override void Execute()
    {
        Output(Directory.GetCurrentDirectory());
    }
}