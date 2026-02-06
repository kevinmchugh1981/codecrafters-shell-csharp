internal class Bash
{
    

    public void Start()
    {
        do
        {
            var input = Keyboard.GetInput();
            if (string.IsNullOrWhiteSpace(input))
                continue;

            var command = CommandFactory.CreateCommand(input);
            command.Execute();
        }
        while (true);
    }
    
}