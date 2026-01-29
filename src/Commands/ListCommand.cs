public class ListCommand(string arguments) : BaseCommand
{
    public override string Arguments { get; } = arguments;
    public override bool CanRedirect => true;

    public override void Execute()
    {
        if(string.IsNullOrWhiteSpace(Arguments))
            return;

        var items = Directory.GetFileSystemEntries(Arguments).Select(Path.GetFileName).OrderBy(x => x)
            .ToList();

        if (items.Count == 0)
            return;
        
        foreach (var item in items.Where(item => !string.IsNullOrWhiteSpace(item)))
        {
            if (Directory.Exists(Path.Combine(Arguments, item!)))
            {
                Output(item + "/ ");
            }
            else
            {
                Output(item + " ");
            }
        }
    }
}