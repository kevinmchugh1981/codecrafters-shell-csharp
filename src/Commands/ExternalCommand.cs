using System.Diagnostics;

public class ExternalCommand(string filePath) : ICommand
{
    public void Execute(string[] args)
    {
        var startInfo = new ProcessStartInfo(filePath);
        if (args.Length > 1)
        {
            startInfo.Arguments = string.Join(" ", args.Skip(1).ToArray());
        }
        Process.Start(startInfo);
    }
}