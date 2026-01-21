using System.Diagnostics;

public class ExternalCommand(string filePath) : ICommand
{
    private string filePath = filePath;


    public void Execute(string[] args)
    {
        var command = Path.GetFileName(filePath);
        var directory = Path.GetDirectoryName(filePath);
        var commandArgs = args.Length > 1 ? string.Join(" ", args.Skip(1).ToArray()) : string.Empty;
        
        var startInfo = new ProcessStartInfo(command, commandArgs)
        {
            WorkingDirectory = directory,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            UseShellExecute = false
        };
        using var process = Process.Start(startInfo);
        var output = process?.StandardOutput.ReadToEnd();
        var error = process?.StandardError.ReadToEnd();
        process?.WaitForExit();
        if(!string.IsNullOrWhiteSpace(output))
            Console.Write(output);
        if(!string.IsNullOrWhiteSpace(error))
            Console.Write(error);
    }
}