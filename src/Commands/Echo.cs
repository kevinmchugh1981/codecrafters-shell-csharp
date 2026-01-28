public class EchoCommand(string arguments) : ICommand
{
    private readonly IParser parser = new ArgumentParser();

    public string Arguments { get; } = arguments;

    public void Execute()
    {
        if (string.IsNullOrWhiteSpace(Arguments))
        {
            Console.WriteLine(string.Empty);
            return;
        }

        Console.Out.WriteLine(string.Join(" ", parser.Parse(Arguments)));
        
    }
}