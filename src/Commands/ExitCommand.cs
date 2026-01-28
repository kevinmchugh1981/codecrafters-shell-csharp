

public class ExitCommand : ICommand
{
    public void Execute(string args)
    {
        System.Environment.Exit(0);
    }
}