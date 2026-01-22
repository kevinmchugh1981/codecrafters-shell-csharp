internal static class StringExtensions
{
    internal static List<string> Parse(this string args)
    {
        if (args.StartsWith('\''))
            return Parse(args, '\'');

        return args.StartsWith('"') ? Parse(args, '\"') : args.Split(" ").ToList();
    }

    private static List<string> Parse(string args, char delimiter)
    {
        var result = new List<string>();

        if (string.IsNullOrWhiteSpace(args) || char.IsWhiteSpace(delimiter))
            return result;

        var currentString = string.Empty;
        for (var x = 0; x < args.Length; x++)
        {
            if (args[x] == delimiter)
            {
                if (x == 0)
                    continue;
                if (string.IsNullOrWhiteSpace(currentString) && currentString.Length > 1)
                    currentString = " ";
                result.Add(currentString);
                currentString = string.Empty;
                continue;
            }

            currentString += args[x];

            if (x != args.Length - 1)
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(currentString) && currentString.Length > 1)
                currentString = " ";
            result.Add(currentString);

        }

        return result;
    }
}