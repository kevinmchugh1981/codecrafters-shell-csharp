public class TypeCommand(string arguments) : BaseCommand
{
    public override string Arguments { get; } = arguments;
    public override bool CanRedirect => false;

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
            Output($"{Arguments}: not found");
        }
    }

    
}