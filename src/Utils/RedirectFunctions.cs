using System.Text;

public static class RedirectFunctions
{
    public static bool Redirect(string args)
    {
        return args.Contains(" > ", StringComparison.InvariantCultureIgnoreCase)
               || args.Contains(" 1> ", StringComparison.InvariantCultureIgnoreCase);
    }

    public static (string, string) Split(string args)
    {
        var splitIndex = -1;
        var currentDelimiter = char.MinValue;
        for (var x = 0; x < args.Length; x++)
        {
            //Inside delimiter, and find delimiter, close current delimiter.
            if (currentDelimiter != char.MinValue && UtilsConstants.Delimiters.Contains(args[x]))
            {
                currentDelimiter = char.MinValue;
                continue;
            }

            //Not inside delimiter, and find delimiter, set current delimiter.
            if (currentDelimiter == char.MinValue && UtilsConstants.Delimiters.Contains(args[x]))
            {
                currentDelimiter = args[x];
            }

            if (currentDelimiter != char.MinValue || args[x] != '>')
            {
                continue;
            }

            //Not inside delimiter, and find redirect symbol, stop.
            splitIndex = x;
            break;
        }

        var twoPartIndex = args[splitIndex - 1] == '1';

        var content = args[..(twoPartIndex ? splitIndex - 1 : splitIndex)];
        var destination = args[(splitIndex + 1)..];
        return (content, destination);
    }

    public static void Write(StringBuilder stringBuilder, string destination)
    {
        try
        {
            if (!Directory.Exists(new FileInfo(destination).Directory?.FullName))
                Directory.CreateDirectory(destination);

            if (File.Exists(destination))
                File.Delete(destination);

            File.WriteAllText(destination, stringBuilder.ToString());
        }
        catch
        {
            // ignored
        }
    }

    public static bool TryParseRedirect(List<string> args, out List<string> content, out string destination)
    {
        content = [];
        destination = string.Empty;
        var lastItem = false;

        foreach (var item in args)
        {
            if (item == ">")
            {
                lastItem = true;
            }
            else if (lastItem)
            {
                destination = item;
                break;
            }
            else
            {
                content.Add(item);
            }
        }


        return content.Any() && File.Exists(destination);
    }
}