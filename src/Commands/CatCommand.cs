using System.Text;

public class CatCommand(string arguments, RedirectType redirectType) : BaseCommand(redirectType, arguments)
{
    
    protected override void Process()
    {
        //Validate files exists
        var content = GetExistingFiles(ArgumentParser.Parse(Arguments));
        if (content.Count == 0)
            return;
        
        //Generate content
        var fileContent= GenerateContent(content);
        Output(string.Join("", fileContent));
    }

    protected override void Redirect()
    {
        
        //Files exists
        var content = GetExistingFiles(ParseRedirect());
        if (content.Count == 0)
            return;
        
        //Generate content
        var stringBuilder = new StringBuilder();
        var fileContent= GenerateContent(content);
        foreach(var line in fileContent)
            stringBuilder.AppendLine(line);

        //Write to file.
        Output(stringBuilder.ToString().Trim());
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
                Output($"cat: {path}: No such file or directory", true);
            else
                results.Add(path);
        }

        return results;
    }
}