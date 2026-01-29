public interface ICommand
{
    string Arguments { get; }
    bool CanRedirect { get; }
    void Execute();
}