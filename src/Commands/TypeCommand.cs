using System.Runtime.InteropServices;

public class TypeCommand(string arguments) : ICommand
{
    public string Arguments { get; } = arguments;

    public void Execute()
    {

        if (Constants.Commands.ContainsKey(Arguments) && Constants.IsShellBuiltIn(Arguments))
        {
            Console.Out.WriteLine($"{Arguments} is a shell builtin");
        }
        else if (FileSearcher.IsExecutable(Arguments, out var filePath, out string parameters))
        {
            Console.Out.WriteLine($"{Arguments} is {filePath}");
        }
        else
        {
            Console.Out.WriteLine($"{Arguments}: not found");
        }
    }

    
}