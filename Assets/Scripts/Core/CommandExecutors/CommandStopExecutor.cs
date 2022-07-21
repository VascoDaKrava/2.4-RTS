using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace Core.CommandExecutors
{
    public sealed class CommandStopExecutor : CommandExecutorBase<IStopCommand>
    {
        [SerializeField] private UnitCTSource _unitCTSource;

        //private void Start()
        //{
        //    _unitCTSource = GetComponent<UnitCTSource>();
        //}

        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            _unitCTSource.Cancel();
        }
    }
}