using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;


namespace Abstractions.Commands.CommandExecutors
{
    public sealed class CommandAttackExecutor : CommandExecutorBase<IAttackCommand>
    {
        public override void ExecuteSpecificCommand(IAttackCommand command)
        {
            Debug.Log($"{name} attack here : {command.Target}");
        }
    }
}