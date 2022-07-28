using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class CommandTeleportExecutor : CommandExecutorBase<ITeleportCommand>
    {
        public async override Task ExecuteSpecificCommand(ICommand baseCommand)
        {
            var command = (ITeleportCommand)baseCommand;
            await Task.Yield();
            DoTeleportation(command.Target);
        }

        private void DoTeleportation(Vector3 target)
        {
            gameObject.transform.position = target;
        }
    }
}