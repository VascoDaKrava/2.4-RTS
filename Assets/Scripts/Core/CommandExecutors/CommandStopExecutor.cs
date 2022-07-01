using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading;
using Zenject;

namespace Core.CommandExecutors
{
    public sealed class CommandStopExecutor : CommandExecutorBase<IStopCommand>
    {
        //[Inject] 
        private UnitCTSource _unitCTSource;

        private void Start()
        {
            _unitCTSource = GetComponent<UnitCTSource>();
        }

        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            _unitCTSource.CTSource?.Cancel();
        }
    }
}