using System.Text;


public enum RedirectType
{
    None,
    Output,
    Error,
    OutputAppend,
    ErrorAppend
}

public static class RedirectFunctions
{
    
    public static RedirectType GetRedirectType(string args)
    {
        if(string.IsNullOrWhiteSpace(args))
            return RedirectType.None;
        
        if(args.Contains(" 2> "))
            return RedirectType.Error;
        if(args.Contains(" 2>> "))
            return RedirectType.ErrorAppend;
        if(args.Contains(" 1> ") || args.Contains(" > "))
            return RedirectType.Output;
        if(args.Contains(" 1>> ") || args.Contains(" >> "))
            return RedirectType.OutputAppend;
        return RedirectType.None;
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

        //Check if it's a two part index.
        var twoPartIndex = args[splitIndex - 1] == '1' ||  args[splitIndex - 1] == '2';
        
        //Check if it's a double redirect symbol.
        var doubleRedirect = false;
        if (args.Length >= splitIndex + 1)
        {
            doubleRedirect = args[splitIndex + 1] == '>';
        }
        
        var content = args[..(twoPartIndex ? splitIndex - 1 : splitIndex)];
        var destination = args[(splitIndex + (doubleRedirect ? 2 : 1))..];
        return (content, destination);
    }
}