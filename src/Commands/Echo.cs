public class EchoCommand : ICommand
{
    public void Execute(string[] args)
    {
        if (args.Length <= 1)
        {
            Console.WriteLine(string.Empty);
            return;
        }
        
        var content = args.Skip(1).ToArray();

        Console.WriteLine(content.All(x => x != "'")
            ? string.Join(" ", content.Where(x => !string.IsNullOrWhiteSpace(x)))
            : string.Join(" ", content.Select(x => x.Replace("'", string.Empty))));
    }
}