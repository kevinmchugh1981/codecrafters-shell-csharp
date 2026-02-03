public class ArgumentParser : IParser
{
    
    
    private static readonly char EscapeDelimiter = '\\';

    public List<string> Parse(string args)
    {

        if (string.IsNullOrWhiteSpace(args))
            return [];
        
        //Concatenate any words that contain double delimiters.
        args = UtilsConstants.Delimiters.Aggregate(args,
            (current, delimiter) => current.Replace($"{delimiter}{delimiter}", string.Empty));
        
        //Check if it's only a list of arguments with not delimiters.
        if(!args.Any(x=> UtilsConstants.Delimiters.Contains(x)) && args.All(x => x != EscapeDelimiter))
            return args.Split().Where(x=> !string.IsNullOrWhiteSpace(x)).ToList();

        var result = new List<string>();
        var currentString = string.Empty;
        var currentDelimiter = char.MinValue;
        var escapeNextChar = false;
        for (var x = 0; x < args.Length; x++)
        {
            //If char is escapable, you are not inside ANY delimiter, and it's not currently being escaped, set it to escape.
            if (args[x] == EscapeDelimiter && currentDelimiter == Char.MinValue && !escapeNextChar)
            {
                escapeNextChar = true;
                continue;
            }
            
            //If char is escapable, you are not in single quotes, and it's not currently being escaped, set it to escape.
            if (args[x] == EscapeDelimiter && currentDelimiter != UtilsConstants.Delimiters[0]  && !escapeNextChar)
            {
                escapeNextChar = true;
                continue;
            }
            
            //Check if char is delimiter
            if (UtilsConstants.Delimiters.Contains(args[x]))
            {
                //If inside delimiter, and current char is not the same as current delimiter - Add char.
                if (currentDelimiter != char.MinValue && (currentDelimiter != args[x] || escapeNextChar))
                {
                    currentString += args[x];
                }
                //If inside delimiter, and this will close it - Store current string.
                else if (currentDelimiter != Char.MinValue && currentDelimiter == args[x])
                {
                    //Only store the string if there are more chars of they are a space.
                    if (x == args.Length - 1 || (x <= args.Length + 1 && char.IsWhiteSpace(args[x + 1])))
                    {
                        result.Add(currentString);
                        currentString = string.Empty;
                        currentDelimiter = char.MinValue;
                    }
                } 
                //If you are outside the delimiter, and you are escaping this character and the char is not whitespace - add char
                else if (currentDelimiter == Char.MinValue && escapeNextChar && !Char.IsWhiteSpace(args[x]))
                {
                    currentString += args[x];
                }
                //If not inside delimiter - Set current delimiter.
                else if (currentDelimiter == char.MinValue)
                {
                    currentDelimiter = args[x];
                }

                //Escapable char should have been added, so unset value.
                escapeNextChar = false;
                
                //If there are no more chars then store the last value.
                if (x == args.Length - 1 && !string.IsNullOrEmpty(currentString))
                    result.Add(currentString);
                
                continue;
            }

            //If you are inside a delimiter, then add any char.
            if (!currentDelimiter.Equals(Char.MinValue))
            {
                //If you are inside a delimiter, don't add further delimiters.
                currentString += (args[x] != currentDelimiter || escapeNextChar) ? args[x] : string.Empty;
            }
            //If you are not inside a delimiter, add char.
            else
            {
                // If char is not being ignored, and it's a whitespace and the current string is empty then store the string.
                if (!string.IsNullOrWhiteSpace(currentString) && char.IsWhiteSpace(args[x]) && !escapeNextChar)
                {
                    result.Add(currentString);
                    currentString=string.Empty;
                }
                else
                //If is not white space, or you aren't escaping it, then add to string.
                    currentString += !char.IsWhiteSpace(args[x]) || escapeNextChar ? args[x] : string.Empty;
            }
            
            //Escapable char should have been added, so unset value.
            escapeNextChar = false;

            //If there are no more chars then store the last value.
            if (x == args.Length - 1 && !string.IsNullOrEmpty(currentString))
                result.Add(currentString);
        }

        return result;
    }
}

