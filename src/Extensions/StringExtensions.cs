internal static class StringExtensions
{
private static List<char> Delimiters = new() { '\'', '"' };

    internal static List<string> Parse(this string args)
    {
        var result = new List<string>();
       
               if (string.IsNullOrWhiteSpace(str))
                   return result;
       
               //Only blank spaces.
               if (str.Contains(Delimiters[0]) || str.Contains(Delimiters[1]))
               {
                   return GetBetweenDelimiters(str);
               }
       
               result = str.Split(" ").ToList().Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
               return result;
    }

   private static List<string> GetBetweenDelimiters(string str)
       {
           var result = new List<string>();
           var insideQuotes = false;
           var currentString = string.Empty;
           var doubleDelimiter = false;
           for (var x = 0; x < str.Length; x++)
           {
               if (Delimiters.Contains(str[x]))
               {
                   insideQuotes = !insideQuotes;
                   if (!insideQuotes)
                   {
                       if (currentString != string.Empty)
                           if (!doubleDelimiter)
                           {
                               result.Add(currentString);
                               currentString = string.Empty;
                           }
                           else
                               doubleDelimiter = false;
                   }
                   else if (x + 1 <= str.Length - 1)
                   {
                       doubleDelimiter = str[x] == str[x + 1];
                   }
               }
               else if (insideQuotes)
               {
                   currentString += str[x];
               }
               else if (!insideQuotes)
               {
                   if (char.IsWhiteSpace(str[x]) && !string.IsNullOrWhiteSpace(currentString))
                   {
                       result.Add(currentString);
                       currentString = string.Empty;
                   }
                   else if (!char.IsWhiteSpace(str[x]))
                   {
                       currentString += str[x];
                   }
               }
   
               if (x == str.Length - 1 && !string.IsNullOrWhiteSpace(currentString))
                   result.Add(currentString);
           }
   
           return result;
       }
}