using System.Runtime.InteropServices;

internal static class FileSearcher
{
    private static readonly IParser Parser = new PathParser();


    internal static bool AutoComplete(string criteria, out List<string> externalCommand)
    {
        externalCommand = new List<string>();

        if (!TryGetDirectories(out var directories))
            return false;

        foreach (var directory in directories)
        {
            if (!Directory.Exists(directory))
                continue;

            foreach (var file in Directory.GetFiles(directory))
            {
                if (Path.GetFileName(file).StartsWith(criteria, StringComparison.InvariantCultureIgnoreCase) &&
                    IsFileExecutable(file))
                    externalCommand.Add(Path.GetFileName(file));
            }
        }

        return externalCommand.Count > 0;
    }

    internal static bool IsExecutable(string command, out string filePath, out string parameters)
    {
        filePath = string.Empty;
        parameters = string.Empty;

        var commandElements = Parser.Parse(command);

        switch (commandElements.Count)
        {
            case 0:
                return false;
            case > 1:
                parameters = string.Join(" ", commandElements.Skip(1));
                break;
        }


        if (!TryGetDirectories(out var directories))
            return false;

        foreach (var directory in directories)
        {
            var fullPath = Path.Combine(directory.Trim(), commandElements[0].Trim());

            if (IsFileExecutable(fullPath))
            {
                filePath = fullPath;
            }
        }

        return !string.IsNullOrWhiteSpace(filePath);
    }

    private static bool TryGetDirectories(out List<string> directories)
    {
        directories = [];
        var pathEnv = Environment.GetEnvironmentVariable("PATH");
        if (string.IsNullOrEmpty(pathEnv)) return false;
        var pathSeparator = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ';' : ':';

        directories = pathEnv.Split(pathSeparator).ToList();
        return directories.Count > 0;
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