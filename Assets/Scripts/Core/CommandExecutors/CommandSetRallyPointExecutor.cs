using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;

namespace Core.CommandExecutors
{
    public sealed class CommandSetRallyPointExecutor : CommandExecutorBase<ISetRallyPointCommand>
    {
        public override void ExecuteSpecificCommand(ICommand baseCommand)
        {
            var command = (ISetRallyPointCommand)baseCommand;
            GetComponent<IHolderRallyPoint>().RallyPoint = command.RallyPoint;
        }
    }
}