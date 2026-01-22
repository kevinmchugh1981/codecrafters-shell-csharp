public class ChangeDirectoryCommand : ICommand
{

    public void Execute(string args)
    {
        switch (string.IsNullOrWhiteSpace(args))
        {
            case false when Directory.Exists(args):
                Directory.SetCurrentDirectory(args);
                break;
            case false when args == "~":
                Directory.SetCurrentDirectory(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
                break;
            default:
                Console.Out.WriteLine($"cd: {args}: No such file or directory");
                break;
        }
    }
}