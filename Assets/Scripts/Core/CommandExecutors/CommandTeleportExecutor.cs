using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core.CommandExecutors
{
    public class CommandTeleportExecutor : CommandExecutorBase<ITeleportCommand>
    {
        [Inject] private IHolderUnitMovementStop _unitMoveStopHolder;

        public async override Task ExecuteSpecificCommand(ICommand baseCommand)
        {
            var command = (ITeleportCommand)baseCommand;
            await Task.Yield();
            _unitMoveStopHolder.DoStop();
            DoTeleportation(command.Target);
        }

        private void DoTeleportation(Vector3 target)
        {
            gameObject.transform.position = target;
        }
    }
}