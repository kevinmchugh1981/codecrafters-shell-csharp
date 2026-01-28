public class PwdCommand : ICommand
{
    public string Arguments { get; }

    public void Execute()
    {
        Console.WriteLine(Directory.GetCurrentDirectory());
    }
}