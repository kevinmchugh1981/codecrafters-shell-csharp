using System.Text.RegularExpressions;

public class CatCommand : ICommand
{
    public void Execute(string args)
    {
        if (string.IsNullOrWhiteSpace(args))
        {
            Console.WriteLine(string.Empty);
            return;
        }

        var content = new List<string>();
        foreach (var path in args.Parse().Where(File.Exists))
        {
            using var stream = new StreamReader(path);
            content.Add(stream.ReadToEnd().Trim());
        }

        Console.WriteLine(string.Join("", content));
    }
    
}