using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;


namespace Abstractions.Commands.CommandExecutors
{
    public sealed class CommandMoveExecutor : CommandExecutorBase<IMoveCommand>
    {
        public override void ExecuteSpecificCommand(IMoveCommand command)
        {
            Debug.Log($"{name} move to {command.Target}");
        }
    }
}