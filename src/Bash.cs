internal class Bash
{
    

    public void Start()
    {
        do
        {
            var input = GetInput();
            if (string.IsNullOrWhiteSpace(input))
                continue;

            var command = CommandFactory.CreateCommand(input);
            command.Execute();
        }
        while (true);
    }
    
    private string GetInput()
    {
        Console.Write("$ ");
        return Console.ReadLine();
    }
    
}