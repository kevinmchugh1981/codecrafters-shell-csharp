public static class Keyboard
{
    public static string GetInput()
    {
        Console.Write("$ ");
        var currentLine = string.Empty;
        ConsoleKeyInfo? previousKey = null;
        while (true)
        {
            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    Console.WriteLine();
                    return currentLine;
                case ConsoleKey.Tab:
                    var command = Constants.Commands.FirstOrDefault(x =>
                        x.Key.StartsWith(currentLine, StringComparison.InvariantCultureIgnoreCase)).Key ?? string.Empty;
                    if (!string.IsNullOrWhiteSpace(command))
                    {
                        command += " ";
                        Console.Write($"\r$ {command}");
                        currentLine = command;
                        break;
                    }
                    if (FileSearcher.AutoComplete(currentLine.Trim(), out var externalCommands) &&
                        previousKey is { Key: ConsoleKey.Tab })
                    {
                        var content = string.Join("  ", externalCommands.OrderBy(x => x));
                        Console.WriteLine();
                        Console.WriteLine(content);
                        Console.Write($"\r$ {currentLine}");
                    }
                    else if (previousKey.HasValue && previousKey.Value.Key != ConsoleKey.Tab && externalCommands.Count == 1)
                    {
                        var externalCommand = externalCommands.First()+ " ";
                        Console.Write($"\r$ {externalCommand}");
                        currentLine = externalCommand;
                    }
                    else if(previousKey.HasValue && previousKey.Value.Key != ConsoleKey.Tab && externalCommands.Any())
                    {
                        Console.Write('\a');
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
    
            previousKey = key;
        }
    }
}   