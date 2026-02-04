public class EchoCommand(string arguments, RedirectType redirectType) : BaseCommand(redirectType, arguments)
{
    
    protected override void Process()
    {
        Output(string.Join(" ", ArgumentParser.Parse(Arguments)));
    }

    protected override void Redirect()
    {
       Output(string.Join(" ", ParseRedirect()));
    }
    
}