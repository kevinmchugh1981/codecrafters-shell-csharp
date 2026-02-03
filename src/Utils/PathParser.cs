public class PathParser : IParser
{
    public List<string> Parse(string args)
    {
        //If nothing return empty set.
        if (string.IsNullOrWhiteSpace(args))
            return new List<string>();

        //If doesn't start with delimiters, then split on space and .
        if (!args.StartsWith("'") && !args.StartsWith("\""))
            return args.Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

        var result = new List<string>();
        var currentString = string.Empty;
        var currentDelimiter = args[0];

        for (var x = 1; x < args.Length; x++)
        {
            //If this char doesn't close delimiter, keeping adding every string.
            if (args[x] != currentDelimiter && currentDelimiter == args[0])
            {
                currentString += args[x];
                continue;
            }

            //if this char closes delimiter, add string and set delimiter to char min.
            if (args[x] == args[0])
            {
                result.Add(currentString);
                currentString = string.Empty;
                currentDelimiter = char.MinValue;
                continue;
            }

            //if this char is space, add string.
            if (currentDelimiter != char.MinValue)
            {
                continue;
            }

            //If this char is white space, add string
            if (char.IsWhiteSpace(args[x]))
            {
                if(!string.IsNullOrWhiteSpace(currentString))
                    result.Add(currentString);
                currentString = string.Empty;
            }
            //If char is not white space, add char.
            else
            {
                currentString += args[x];
            }

            //Add existing string when hitting end.
            if (x == args.Length - 1)
            {
                if(!string.IsNullOrWhiteSpace(currentString))
                    result.Add(currentString);
            }
        }

        return result;
    }
}