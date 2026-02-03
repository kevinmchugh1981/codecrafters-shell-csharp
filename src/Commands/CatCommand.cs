using System.Text;

public class CatCommand(string arguments) : BaseCommand
{
    private readonly IParser argumentParser = new ArgumentParser();

    public override string Arguments { get; } = arguments;
    public override bool CanRedirect => true;

    public override void Execute()
    {
        if (string.IsNullOrWhiteSpace(Arguments))
        {
            Console.WriteLine(string.Empty);
            return;
        }

        if (CanRedirect && RedirectFunctions.Redirect(Arguments))
            Redirect();
        else
            ToScreen();
    }

    private void ToScreen()
    {
        //Load content
        var content = argumentParser.Parse(Arguments);
        
        //Validate files exists
        content = GetExistingFiles(content);
        if (content.Count == 0)
            return;
        
        //Generate content
        var fileContent= GenerateContent(content);
        Console.WriteLine(string.Join("", fileContent));
    }

    private void Redirect()
    {
        //Split on delimiter
        var splitArguments = RedirectFunctions.Split(Arguments);

        //Parse before delimiter.
        var content = argumentParser.Parse(splitArguments.Item1);

        //Parse destination.
        var destination = argumentParser.Parse(splitArguments.Item2).First();

        //Files exists
        content = GetExistingFiles(content);
        if (content.Count == 0)
            return;
        
        //Generate content
        var stringBuilder = new StringBuilder();
        var fileContent= GenerateContent(content);
        foreach(var line in fileContent)
            stringBuilder.AppendLine(line);

        //Write to file.
        RedirectFunctions.Write(stringBuilder, destination);
    }

    private List<string> GenerateContent(List<string> target)
    {
        var content = new List<string>();
        foreach (var path in target)
        {
            using var stream = new StreamReader(path);
            content.Add(stream.ReadToEnd().Trim());
        }

        return content;
    }

    private List<string> GetExistingFiles(List<string> paths)
    {
        var results = new List<string>();
        foreach (var path in paths)
        {
            if(!File.Exists(path))
                Console.WriteLine($"cat: {path}: No such file or directory");
            else
                results.Add(path);
        }

        return results;
    }
}