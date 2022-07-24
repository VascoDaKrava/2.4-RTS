using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.CommandExecutors
{
    public sealed class CommandAttackExecutor : CommandExecutorBase<IAttackCommand>
    {
        public override async Task ExecuteSpecificCommand(ICommand baseCommand)
        {
            await Task.Yield();
            Debug.Log($"{name} attack");
        }
    }
}