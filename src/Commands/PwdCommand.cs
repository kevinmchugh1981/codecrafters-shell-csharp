public class PwdCommand : BaseCommand
{
    public override string Arguments => string.Empty;
    public override bool CanRedirect => false;


    public override void Execute()
    {
        Output(Directory.GetCurrentDirectory());
    }
}