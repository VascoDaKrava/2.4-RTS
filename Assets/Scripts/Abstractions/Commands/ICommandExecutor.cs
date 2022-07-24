using System.Threading.Tasks;

namespace Abstractions.Commands
{
    public interface ICommandExecutor<ICommand>
    {
        Task ExecuteSpecificCommand(ICommand command);
    }
}