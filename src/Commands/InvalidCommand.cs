public class InvalidCommand(string command) : ICommand
{
    public void Execute(string args)
    {
        Console.Out.WriteLine($"{command}: command not found");
    }
}