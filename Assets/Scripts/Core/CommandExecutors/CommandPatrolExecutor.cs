using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace Core.CommandExecutors
{
    public sealed class CommandPatrolExecutor : CommandExecutorBase<IPatrolCommand>
    {
        public override void ExecuteSpecificCommand(ICommand baseCommand)
        {
            var command = (IPatrolCommand)baseCommand;
            Debug.Log($"{name} start patrol from {command.StartPoint} to {command.FinishPoint}");
        }
    }
}