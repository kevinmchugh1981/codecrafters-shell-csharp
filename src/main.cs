class Program
{
    static void Main()
    {
        for (string? command; (command = GetInput()) != "exit";)
        {
            var arguments = ParseCommand(command);
            if (arguments.Length == 0)
                continue;
            if (arguments[0] == "echo")
                Echo(arguments);
            else
                Console.Out.WriteLine($"{command}: command not found");
        }
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