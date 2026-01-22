public class ChangeDirectoryCommand : ICommand
{

    public void Execute(string[] args)
    {
        switch (args.Length)
        {
            case > 1 when !string.IsNullOrWhiteSpace(args[1]) && Directory.Exists(args[1]):
                Directory.SetCurrentDirectory(args[1]);
                break;
            case > 1 when !string.IsNullOrWhiteSpace(args[1]) && args[1] == "~":
                Directory.SetCurrentDirectory(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
                break;
            default:
                Console.Out.WriteLine($"{args[0]}: {args[1]}: No such file or directory");
                break;
        }
    }
}