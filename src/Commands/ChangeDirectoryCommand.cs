public class ChangeDirectoryCommand : ICommand
{

    public void Execute(string args)
    {
        switch (args.Length)
        {
            case > 1 when !string.IsNullOrWhiteSpace(args) && Directory.Exists(args):
                Directory.SetCurrentDirectory(args);
                break;
            case > 1 when !string.IsNullOrWhiteSpace(args) && args == "~":
                Directory.SetCurrentDirectory(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
                break;
            default:
                Console.Out.WriteLine($"cd: {args}: No such file or directory");
                break;
        }
    }
}