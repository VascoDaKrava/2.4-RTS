using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading.Tasks;

namespace Core.CommandExecutors
{
    public sealed class CommandSetRallyPointExecutor : CommandExecutorBase<ISetRallyPointCommand>
    {
        public override async Task ExecuteSpecificCommand(ICommand baseCommand)
        {
            await Task.Yield();
            var command = (ISetRallyPointCommand)baseCommand;
            GetComponent<IHolderRallyPoint>().RallyPoint = command.RallyPoint;
        }
    }
}