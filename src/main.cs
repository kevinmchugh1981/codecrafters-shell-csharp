class Program
{
    static void Main()
    {
        var test = false;

        if (test)
        {
            var tests = new BashTests();
            tests.Execute();
        }
        else
        {
            var bash = new Bash();
            bash.Start();
        }
        
       
    }



}