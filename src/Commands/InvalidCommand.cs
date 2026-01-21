public class InvalidCommand : ICommand
{
    public void Execute(string[] command)
    {
        Console.Out.WriteLine($"{command[0]}: command not found");
    }
}