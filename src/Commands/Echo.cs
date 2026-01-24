using System.Text.RegularExpressions;

public class EchoCommand : ICommand
{
    public void Execute(string args)
    {
        if (string.IsNullOrWhiteSpace(args))
        {
            Console.WriteLine(string.Empty);
            return;
        }

        Console.Out.WriteLine(string.Join(" ", args.Parse()));
        
    }
}