public abstract class BaseCommand : ICommand
{

    public abstract string Arguments { get; }
    public abstract bool CanRedirect { get; }
    public abstract void Execute();
    
}