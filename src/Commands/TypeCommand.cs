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
 
        var pathEnv = Environment.GetEnvironmentVariable("PATH");
        if (string.IsNullOrEmpty(pathEnv)) return false;
        var pathSeparator = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ';' : ':';
        
        var directories = pathEnv.Split(pathSeparator);
        foreach (var directory in directories)
        {
            var fullPath = Path.Combine(directory.Trim(), command);
            
            if (IsFileExecutable(fullPath))
            {
                filePath = fullPath;
            }
        }
        return !string.IsNullOrWhiteSpace(filePath);
    }

    private static bool IsFileExecutable(string filePath)
    {
        if (!File.Exists(filePath)) return false;

         if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var ext = Path.GetExtension(filePath).ToLower();
            string[] executableExts = { ".exe", ".bat", ".cmd", ".ps1" };
            return executableExts.Contains(ext);
        }

        var fileInfo = new FileInfo(filePath); 
        
        return (fileInfo.UnixFileMode & (UnixFileMode.UserExecute | 
                                         UnixFileMode.GroupExecute | 
                                         UnixFileMode.OtherExecute)) != 0;
    }
}