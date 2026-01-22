internal static class Extensions
{
    internal static List<string> ParseStrings(this string[] args)
    {
        return args.All(x => !x.Contains('\''))
            ? args.Where(x => !string.IsNullOrWhiteSpace(x)).ToList()
            : args.Select(x => x.Replace("'", string.Empty)).ToList();
    }
}