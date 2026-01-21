using System.Runtime.InteropServices;

public class TypeCommand : ICommand
{

    public void Execute(string[] command)
    {
        switch (command)
        {
            case ["type", _] when CommandsEnum.Commands.Contains(command[1]):
                Console.Out.WriteLine($"{command[1]} is a shell builtin");
                break;
            case ["type", _] when IsExecutable(command[1], out var filePath):
            {
                Console.Out.WriteLine($"{command[1]} is {filePath}");
                break;
            }
            default:
                Console.Out.WriteLine($"{command[1]}: not found");
                break;
        }
    }

    private bool IsExecutable(string command, out string filePath)
    {
        filePath = string.Empty;
        var pathSeparator = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ';' : ':';
        var pathEnv = Environment.GetEnvironmentVariable("PATH");
        if (string.IsNullOrEmpty(pathEnv)) return false;
        var extensions = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) 
            ? new[] { ".exe", ".bat", ".cmd", "" } 
            : new[] { "" };
        
        var directories = pathEnv.Split(pathSeparator);
        foreach (var directory in directories)
        {
            foreach (var extension in extensions)
            {
                var fullPath = Path.Combine(directory.Trim(), command + extension);
                if (File.Exists(fullPath))
                {
                    filePath = Path.GetFullPath(fullPath);
                }
            }
        }
        return !string.IsNullOrWhiteSpace(filePath);
    }
}