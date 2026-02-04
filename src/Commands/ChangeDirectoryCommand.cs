public class ChangeDirectoryCommand(string arguments) : BaseCommand(RedirectType.None, arguments)
{
    
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
                Output($"cd: {Arguments}: No such file or directory", true);
                break;
        }
    }
}