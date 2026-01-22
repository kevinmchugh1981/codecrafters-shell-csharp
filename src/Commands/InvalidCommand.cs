public class InvalidCommand(string command) : ICommand
{
    public void Execute(string args)
    {
        Console.Out.WriteLine($"{command[0]}: command not found");
    }
}