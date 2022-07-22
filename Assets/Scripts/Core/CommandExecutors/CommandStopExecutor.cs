using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using Zenject;

namespace Core.CommandExecutors
{
    public sealed class CommandStopExecutor : CommandExecutorBase<IStopCommand>
    {
        [Inject] private UnitCTSource _unitCTSource;

        public override void ExecuteSpecificCommand(ICommand command)
        {
            _unitCTSource.Cancel();
        }
    }
}