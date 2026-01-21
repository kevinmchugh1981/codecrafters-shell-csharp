public class EchoCommand : ICommand
{
    public void Execute(string[] input)
    {
        var content = input.Skip(1).ToArray();
        Console.WriteLine(string.Join(" ", content));
    }
}