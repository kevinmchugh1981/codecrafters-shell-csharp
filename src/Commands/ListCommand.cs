using System.Text;

public class ListCommand : BaseCommand
{
    private bool isSingleColumn;
    
    public ListCommand(string arguments, RedirectType redirectType) : base(redirectType, arguments)
    {
        this.Arguments = arguments;
        if (!arguments.StartsWith(Constants.ListSwitch))
        {
            return;
        }

        isSingleColumn = true;
        this.Arguments = arguments[2..];
    }

    protected override void Redirect()
    {
        //Generate output
        var builder = new StringBuilder();
        foreach (var item in GenerateContent(ParseRedirect().First()))
            builder.AppendLine(item);

        //Write to file.
        Output(builder.ToString());
    }

    private List<string> GenerateContent(string path)
    {
        if (!Directory.Exists(path))
        {
            Output($"ls: {path}: No such file or directory",true);
            return [];
        }

        ;

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