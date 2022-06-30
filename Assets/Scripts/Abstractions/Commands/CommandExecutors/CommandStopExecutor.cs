using Abstractions.Commands.CommandsInterfaces;
using System.Threading;
using Zenject;

namespace Abstractions.Commands.CommandExecutors
{
    public sealed class CommandStopExecutor : CommandExecutorBase<IStopCommand>
    {
        [Inject] private CancellationTokenSource _ctSource;

        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            _ctSource?.Cancel();
        }
    }
}