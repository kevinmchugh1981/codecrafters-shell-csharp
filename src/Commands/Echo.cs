using System.Text;

public class EchoCommand(string arguments) : BaseCommand
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

        var args = parser.Parse(Arguments);
        
        if (CanRedirect && SharedFunctions.Redirect(args))
        {
            var redirect =  SharedFunctions.ToRedirect(args);

            if (!Directory.Exists(Path.GetFullPath(redirect.Item2)))
            {
                Output("echo: nonexistent: No such file or directory");
            }
            
            if(File.Exists(redirect.Item2))
                File.Delete(redirect.Item2);
            var builder = new StringBuilder();
            foreach (var item in redirect.Item1)
            {
                builder.Append(item);
            }
            File.WriteAllText(redirect.Item2, builder.ToString());
        }
        else
        Output(string.Join(" ", args));
    }
}