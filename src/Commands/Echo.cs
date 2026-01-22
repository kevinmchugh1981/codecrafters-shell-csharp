public class EchoCommand : ICommand
{
    public void Execute(string[] args)
    {
        if (args.Length <= 1)
        {
            Console.WriteLine(string.Empty);
            return;
        }

        var content = args.Skip(1).ToArray();
        Console.WriteLine(string.Join(" ",  content.ParseStrings()));

    }
}