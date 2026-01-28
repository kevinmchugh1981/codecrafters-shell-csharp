using System.Diagnostics;

public class ExternalCommand( string filePath,string arguments) : ICommand
{
    public string Arguments { get; } = arguments;
    private string FilePath { get; } = filePath;

    public void Execute()
    {
        var command = Path.GetFileName(FilePath);
        var directory = Path.GetDirectoryName(FilePath);
        
        var startInfo = new ProcessStartInfo(command, Arguments)
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