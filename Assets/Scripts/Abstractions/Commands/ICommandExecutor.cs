namespace Abstractions.Commands
{
    public interface ICommandExecutor<ICommand>
    {
        void ExecuteSpecificCommand(ICommand command);
    }
}