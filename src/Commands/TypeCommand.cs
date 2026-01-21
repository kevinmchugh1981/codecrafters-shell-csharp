using System.Runtime.InteropServices;

public class TypeCommand : ICommand
{

    public void Execute(string[] command)
    {
        switch (command)
        {
            case ["type", _] when CommandsEnum.Commands.Contains(command[1]):
                Console.Out.WriteLine($"{command[1]} is a shell builtin");
                break;
            case ["type", _] when FileSearcher.IsExecutable(command[1], out var filePath):
            {
                Console.Out.WriteLine($"{command[1]} is {filePath}");
                break;
            }
            default:
                Console.Out.WriteLine($"{command[1]}: not found");
                break;
        }
    }

    
}