public class ChangeDirectoryCommand : ICommand
{

    public void Execute(string[] args)
    {
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]) && Directory.Exists(args[1]))
        {
            Directory.SetCurrentDirectory(args[1]);
        }
        else
            Console.Out.WriteLine($"{args[0]}: {args[1]}: No such file or directory");
    }
}