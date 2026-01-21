class Program
{
    private static readonly string[] Commands =
    [
        "echo",
        "exit",
        "type"
    ];
    
    static void Main()
    {
        for (string? command; (command = GetInput()) != "exit";)
        {
            var arguments = ParseCommand(command);

            if (arguments.Length == 0)
                continue;

            switch (arguments[0])
            {
                case "echo":
                    Echo(arguments);
                    break;
                case "type":
                    Type(arguments);
                    break;
                default:
                    Console.Out.WriteLine($"{command}: command not found");
                    break;
            }
        }
    }

    private static void Type(string[] command)
    {
        if (command is ["type", _] && Commands.Contains(command[1]))
            Console.Out.WriteLine($"{command[1]} is a shell builtin");
        else
            Console.Out.WriteLine($"{command[1]}: not found");
    }

    private static string? GetInput()
    {
        Console.Write("$ ");
        return Console.ReadLine();
    }

    private static void Echo(string[] input)
    {
        var content = input.Skip(1).ToArray();
        Console.WriteLine(string.Join(" ", content));
    }

    private static string[] ParseCommand(string? input)
    {
        return string.IsNullOrWhiteSpace(input) ? [] : input.Split(' ');
    }
}