using System.Threading.Tasks;

namespace Abstractions.Commands
{
    public interface ICommandExecutor<ICommand> : IMotor
    {
        Task TryExecuteCommand(object command);
    }
}