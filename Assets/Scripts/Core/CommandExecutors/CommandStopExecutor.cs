using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading.Tasks;
using Zenject;

namespace Core.CommandExecutors
{
    public sealed class CommandStopExecutor : CommandExecutorBase<IStopCommand>
    {
        [Inject] private UnitCTSource _unitCTSource;

        public override async Task ExecuteSpecificCommand(ICommand command)
        {
            await Task.Yield();
            _unitCTSource.Cancel();
        }
    }
}