using System.Runtime.InteropServices;

internal static class FileSearcher
{
    internal static bool IsExecutable(string command, out string filePath)
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