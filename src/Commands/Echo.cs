using System.Text;

public class EchoCommand(string arguments) : BaseCommand
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
            Console.Out.WriteLine(string.Join(" ", argumentParser.Parse(Arguments)));
    }
    
    private void Redirect()
    {
        //Split on delimiter
        var splitArguments = RedirectFunctions.Split(Arguments);
        
        //Parse before delimiter.
        var content = argumentParser.Parse(splitArguments.Item1);
        
        //Parse destination.
        var destination = argumentParser.Parse(splitArguments.Item2).First();
        
        //Generate output
        var stringBuilder = new StringBuilder();
        foreach(var line in content)
            stringBuilder.AppendLine(line);
        
        //Write to file.
       RedirectFunctions.Write(stringBuilder, destination);
    }
    
}