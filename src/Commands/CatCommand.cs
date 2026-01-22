using System.Reflection.Metadata;

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
        foreach (var path in args.Skip(1).Select(x=> x.Replace("'", string.Empty)))
        {
            if (!File.Exists(path))
            {
                continue;
            }

            using var stream = new StreamReader(path);
            content.Add(stream.ReadToEnd());
        }
        
        Console.WriteLine(string.Join(" ", content));
    }
}