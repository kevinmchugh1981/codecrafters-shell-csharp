using System.Text.RegularExpressions;

public abstract class BaseCommand(RedirectType redirectType, string arguments) : ICommand
{
    public string Arguments { get; protected init; } = arguments;
    private readonly IParser pathParser = new PathParser();
    protected readonly IParser ArgumentParser = new ArgumentParser();
    private RedirectType RedirectType { get; } = redirectType;
    private string? destination;

    public virtual void Execute()
    {
        if (string.IsNullOrWhiteSpace(Arguments))
        {
            Console.WriteLine(string.Empty);
            return;
        }

        if (RedirectType != RedirectType.None)
            Redirect();
        else
            Process();

    }
    
    protected virtual void Process()
    {
    }

    protected virtual void Redirect()
    {
    }

    protected List<string> ParseRedirect()
    {
        //Split arguments.
        var splitArguments = RedirectFunctions.Split(Arguments);

        //Parse input path
        var target = ArgumentParser.Parse(splitArguments.Item1);

        //Parse output path.
        destination = pathParser.Parse(splitArguments.Item2).First();
        
        //Create destination if it doesn't exist.
        CreateFile();
        
        return target;
    }

    protected void Output(string text, bool isError = false)
    {
        switch (RedirectType)
        {
            //Redirect only errors, and is error, write to file.
            case RedirectType.Error or RedirectType.ErrorAppend when isError:
                ToFile(text);
                break;
            //Redirect is output, and is error, write to screen.
            case RedirectType.Output or RedirectType.OutputAppend when isError:
                ToScreen(text);
                break;
            //Redirect is output, and it's not an error, write to screen.
            case RedirectType.Output or RedirectType.OutputAppend when !isError:
                ToFile(text);
                break;
            default:
                ToScreen(text);
                break;
        }
    }

    protected virtual void ToScreen(string text)
    {
        if (!string.IsNullOrWhiteSpace(text))
            Console.WriteLine(text);
    }

    private void ToFile(string text)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(destination))
                return;

            switch (RedirectType)
            {
                case RedirectType.Output:
                case RedirectType.Error:
                    File.WriteAllText(destination, text);
                    break;
                case RedirectType.OutputAppend:
                case RedirectType.ErrorAppend:
                    if(new FileInfo(destination).Length > 0)
                        text = Environment.NewLine + text;
                    File.AppendAllText(destination, text);
                    break;
            }
        }
        catch
        {
            // ignored
        }
    }

    private void CreateFile()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(destination))
                return;

            if (!Directory.Exists(new FileInfo(destination).Directory?.FullName))
                Directory.CreateDirectory(destination);

            if (!File.Exists(destination))
            {
                File.CreateText(destination).Close();
            }
        }
        catch
        {
            // ignored
        }
    }
}