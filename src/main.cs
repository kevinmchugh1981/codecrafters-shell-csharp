class Program
{
    static void Main()
    {
         Console.Write("$ ");
         var command = Console.ReadLine();
         Console.Out.WriteLine($"{command}: command not found");
    }
}
