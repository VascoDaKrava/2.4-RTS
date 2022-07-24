using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.CommandExecutors
{
    public sealed class CommandPatrolExecutor : CommandExecutorBase<IPatrolCommand>
    {
        public override async Task ExecuteSpecificCommand(ICommand baseCommand)
        {
            await Task.Yield();
            var command = (IPatrolCommand)baseCommand;
            Debug.Log($"{name} start patrol from {command.StartPoint} to {command.FinishPoint}");
        }
    }
}