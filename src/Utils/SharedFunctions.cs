public static class SharedFunctions
{
    public static bool Redirect(List<string> args)
    {
        return  args.Any(arg => arg.Equals(">", StringComparison.InvariantCultureIgnoreCase) 
                                || arg.Equals("1>", StringComparison.InvariantCultureIgnoreCase));
    }

    public static (List<string>, string) ToRedirect(List<string> args)
    {
        var elements = new List<string>();
        var path = string.Empty;
        var lastItem = false;

        foreach (var item in args)
        {
            if (item == ">")
            {
                lastItem = true;
            }
            else if (lastItem)
            {
                path = item;
                break;
            }
            else
            {
                elements.Add(item);
            }
        }
        
        return (elements, path);
    }
}