using System.Text;

public class ListCommand(string arguments) : BaseCommand
{
    private IParser argumentParser = new ArgumentParser();
    private bool isSingleColumn;
    
    private string arguments = arguments;
    public override string Arguments => arguments;
    
    public override bool CanRedirect => true;

    public override void Execute()
    {
        if(string.IsNullOrWhiteSpace(Arguments))
            return;

        if (Arguments.StartsWith(Constants.ListSwitch))
        {
            isSingleColumn = true;
            arguments =  Arguments.Substring(2);
        }
        
        if (CanRedirect && RedirectFunctions.Redirect(Arguments))
            Redirect();
        else
            foreach (var item in GenerateContent(Arguments))
                Console.WriteLine(item);

    }

    private void Redirect()
    {
        //Split arguments.
        var splitArguments = RedirectFunctions.Split(Arguments);
        
        //Parse input path
        var target = argumentParser.Parse(splitArguments.Item1).First();

        //Parse output path.
        var output = argumentParser.Parse(splitArguments.Item2).First();

        //Generate output
        var builder = new StringBuilder();
        foreach(var item in GenerateContent(target))
            builder.AppendLine(item);

        //Write to file.
        RedirectFunctions.Write(builder,  output);

    }

    private  List<string> GenerateContent(string path)
    {
        var items = Directory.GetFileSystemEntries(path).Select(Path.GetFileName).OrderBy(x => x)
            .ToList();

        if (items.Count == 0)
            return [];
        
        var result = new List<string>();
        foreach (var item in items.Where(item => !string.IsNullOrWhiteSpace(item)))
        {
            if (Directory.Exists(Path.Combine(path, item!)))
            {
                result.Add(item + "/ ");
            }
            else
            {
                result.Add(item + " ");
            }
        }
        return result;
    }
}