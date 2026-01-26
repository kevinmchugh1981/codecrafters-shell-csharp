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
        foreach (var path in Parse(args).Where(File.Exists))
        {
            using var stream = new StreamReader(path);
            content.Add(stream.ReadToEnd().Trim());
        }

        Console.WriteLine(string.Join("", content));
    }

    private List<string> Parse(string args)
    {
        args = Regex.Replace(args, @"(\\{2})|\\", m => m.Value == @"\\" ? @"\" : "");
        return args.Split(" ").ToList().Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
    }
}