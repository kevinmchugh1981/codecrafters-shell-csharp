class Program
{
    static void Main()
    {
        
        for (string? command; (command= GetInput()) != "exit";)
        {
            Console.Out.WriteLine($"{command}: command not found");
        }
        
    }

    private static string? GetInput()
    {
        Console.Write("$ ");
        return Console.ReadLine();
    }
}
