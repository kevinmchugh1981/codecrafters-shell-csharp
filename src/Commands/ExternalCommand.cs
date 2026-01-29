using System.Diagnostics;

public class ExternalCommand( string filePath,string arguments) : BaseCommand
{
    public override string Arguments { get; } = arguments;
    public override bool CanRedirect => false;
    private string FilePath { get; } = filePath;

    public override void Execute()
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
            Output(output);
        if(!string.IsNullOrWhiteSpace(error))
            Output(error);
    }
}