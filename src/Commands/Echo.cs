using System.Text.RegularExpressions;

public class EchoCommand : ICommand
{
    private readonly IParser parser = new ArgumentParser();
    
    public void Execute(string args)
    {
        if (string.IsNullOrWhiteSpace(args))
        {
            Console.WriteLine(string.Empty);
            return;
        }

        Console.Out.WriteLine(string.Join(" ", parser.Parse(args)));
        
    }
}