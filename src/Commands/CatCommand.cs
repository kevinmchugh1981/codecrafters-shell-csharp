public class CatCommand(string arguments) : BaseCommand
{

    private readonly IParser parser = new ArgumentParser();

    public override string Arguments { get; } = arguments;
    public override bool CanRedirect => true;

    public override void Execute()
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

        Output( string.Join("", content));
    }
    
}