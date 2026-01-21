using System.Diagnostics;

public class ExternalCommand() : ICommand
{
    public void Execute(string[] args)
    {
        var command = args[0];
        var commandArgs = args.Length > 1 ? string.Join(" ", args.Skip(1).ToArray()) : string.Empty;
        
        var startInfo = new ProcessStartInfo("/bin/sh", $"{command} {commandArgs}")
        {
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            UseShellExecute = false
        };
        using var process = Process.Start(startInfo);
        var output = process?.StandardOutput.ReadToEnd();
        var error = process?.StandardError.ReadToEnd();
        process?.WaitForExit();
        if(!string.IsNullOrWhiteSpace(output))
            Console.WriteLine(output);
        if(!string.IsNullOrWhiteSpace(error))
            Console.WriteLine(error);
    }
}