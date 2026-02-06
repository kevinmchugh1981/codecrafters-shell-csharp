public static class Keyboard
{
    public static string GetInputSimple()
    {
        Console.Write("$ ");
        return Console.ReadLine();
    }

    public static string GetInput()
    {
        Console.Write("$ ");
        var currentLine = string.Empty;
        while (true)
        {
            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    Console.WriteLine();
                    return currentLine;
                case ConsoleKey.Tab:
                    var command = Constants.Commands.FirstOrDefault(x=> x.Key.StartsWith(currentLine,  StringComparison.InvariantCultureIgnoreCase)).Key ?? string.Empty;
                    if (!string.IsNullOrWhiteSpace(command))
                    {
                        command += " ";
                        Console.Write($"\r$ {command}");
                        currentLine = command;
                    }
                    else
                    {
                        Console.Write('\a');
                        currentLine += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                    break;
                case ConsoleKey.Backspace:
                {
                    if (currentLine.Length > 0)
                    {
                        currentLine = currentLine.Remove(currentLine.Length - 1);
                        Console.Write("\b \b");
                    }

                    break;
                }
                default:
                    //Standard output
                    currentLine += key.KeyChar;
                    Console.Write(key.KeyChar);
                    break;
            }
        }
    }
    
    
}