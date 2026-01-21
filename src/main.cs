class Program
{
    static void Main()
    {
        for (string? input; (input = GetInput()) != "exit";)
        {
            var arguments = ParseCommand(input);

            if (arguments.Length == 0)
                continue;

            var command = CommandFactory.CreateCommand(arguments[0]);
            command.Execute(arguments);
        }
    }


    private static string? GetInput()
    {
        Console.Write("$ ");
        return Console.ReadLine();
    }


    private static string[] ParseCommand(string? input)
    {
        return string.IsNullOrWhiteSpace(input) ? [] : input.Split(' ');
    }
}