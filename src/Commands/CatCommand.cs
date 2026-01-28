using System.Text.RegularExpressions;

public class CatCommand(string arguments) : ICommand
{

    private readonly IParser parser = new ArgumentParser();

    public string Arguments { get; } = arguments;

    public void Execute()
    {
        if (string.IsNullOrWhiteSpace(Arguments))
        {
            Console.WriteLine(string.Empty);
            return;
        }

        var content = new List<string>();
        foreach (var path in parser.Parse(Arguments).Where(File.Exists))
        {
            using var stream = new StreamReader(path);
            content.Add(stream.ReadToEnd().Trim());
        }

        Console.WriteLine(string.Join("", content));
    }
    
}