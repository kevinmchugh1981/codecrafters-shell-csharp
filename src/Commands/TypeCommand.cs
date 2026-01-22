using System.Runtime.InteropServices;

public class TypeCommand : ICommand
{

    public void Execute(string args)
    {
        if (FileSearcher.IsExecutable(args, out var filePath))
        {
            Console.Out.WriteLine($"{args} is {filePath}");
        }
        else if (CommandsEnum.Commands.ContainsKey(args))
        {
            Console.Out.WriteLine($"{args} is a shell builtin");
        }
        else
        {
            Console.Out.WriteLine($"{args}: not found");
        }
    }

    
}