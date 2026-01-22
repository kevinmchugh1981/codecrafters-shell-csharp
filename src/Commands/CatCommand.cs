public class CatCommand :ICommand
{
    public void Execute(string[] args)
    {
        if (args.Length <= 1)
        {
            Console.WriteLine(string.Empty);
            return;
        }

        var content = new List<string>();
        var paths = args.Skip(1).ToArray().ParseStrings();
        foreach (var path in paths.Where(File.Exists))
        {
            using var stream = new StreamReader(path);
            content.Add(stream.ReadToEnd());
        }
        
        Console.WriteLine(string.Join(" ", content));
    }
}