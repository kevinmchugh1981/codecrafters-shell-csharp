public class ChangeDirectoryCommand(string args) : ICommand
{
    public string Arguments { get; } = args;

    public void Execute()
    {
        switch (string.IsNullOrWhiteSpace(Arguments))
        {
            case false when Directory.Exists(Arguments):
                Directory.SetCurrentDirectory(Arguments);
                break;
            case false when Arguments == "~":
                Directory.SetCurrentDirectory(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
                break;
            default:
                Console.Out.WriteLine($"cd: {Arguments}: No such file or directory");
                break;
        }
    }
}