using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace Core.CommandExecutors
{
    public sealed class CommandAttackExecutor : CommandExecutorBase<IAttackCommand>
    {
        public override void ExecuteSpecificCommand(ICommand baseCommand)
        {
            Debug.Log($"{name} attack");
        }
    }
}