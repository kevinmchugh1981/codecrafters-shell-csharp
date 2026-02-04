public class TypeCommand(string arguments) : BaseCommand(RedirectType.None, arguments)
{
    
    public override void Execute()
    {

        if (Constants.Commands.ContainsKey(Arguments) && Constants.IsShellBuiltIn(Arguments))
        {
            Output($"{Arguments} is a shell builtin");
        }
        else if (FileSearcher.IsExecutable(Arguments, out var filePath, out string parameters))
        {
            Output($"{Arguments} is {filePath}");
        }
        else
        {
            Output($"{Arguments}: not found", true);
        }
    }

    
}