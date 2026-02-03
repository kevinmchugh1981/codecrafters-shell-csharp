

public class BashTests
{
    
    private static readonly Dictionary<string, IParser> Files = new()
    {
       // {"src/Tests/cat.txt", new ArgumentParser()},
       // {"src/Tests/echo.txt", new ArgumentParser()}
       {"src/Tests/externalfile.txt", new ArgumentParser()}
    };

    
    public void Execute()
    {
        foreach (var file in Files)
        {
            using var fileStream = File.OpenRead(file.Key);
            using var reader = new StreamReader(fileStream);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                Console.Out.WriteLine(string.Join(" ",file.Value.Parse(line!)));
            }
        }

        
    }

}