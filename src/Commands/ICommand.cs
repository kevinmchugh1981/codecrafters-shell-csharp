public interface ICommand
{
    string Arguments { get; }
    void Execute();
}