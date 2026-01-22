internal static class StringExtensions
{

    internal static List<string> Parse(this string  args)
    {
        if (args.Contains('\''))
            return Parse(args, '\'');

        return args.Contains('"') ? Parse(args, '\"') : args.Split(" ").ToList();
    }

    private static List<string> Parse(string args, char delimiter)
    {
        var result = new List<string>();
        
        if (string.IsNullOrWhiteSpace(args) || char.IsWhiteSpace(delimiter))
            return result;
        
        var addChar = false;
        var currentString = string.Empty;
        for(var x=0; x<args.Length;x++ )
        {
            if (args[x] == delimiter && addChar)
            {
                result.Add(currentString);
                currentString = string.Empty;
                addChar = false;
                continue;
            }

            if (args[x] == delimiter && !addChar)
            {
                addChar = true;
                continue;
            }
            
            if(args[x] != delimiter && addChar)
                currentString += args[x];

            if (x == args.Length - 1 && addChar)
            {
                result.Add(currentString);
            }
        }
        
        return result;
    }
    
    
}