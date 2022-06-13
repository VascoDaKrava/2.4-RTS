using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;


namespace Abstractions.Commands.CommandExecutors
{
    public sealed class CommandPatrolExecutor : CommandExecutorBase<IPatrolCommand>
    {
        public override void ExecuteSpecificCommand(IPatrolCommand command)
        {
            Debug.Log($"{name} start patrol from {command.StartPoint} to {command.FinishPoint}");
        }
    }
}