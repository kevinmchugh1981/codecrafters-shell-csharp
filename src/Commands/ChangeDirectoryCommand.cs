public class ChangeDirectoryCommand(string args) : BaseCommand
{
    public override string Arguments { get; } = args;
    public override bool CanRedirect => false;

    public override void Execute()
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