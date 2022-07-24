using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.Commands
{
    public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor<ICommand> where T : ICommand
    {
        public async Task TryExecuteCommand(object command)
        {
            if (command is T specificCommand)
            {
                await ExecuteSpecificCommand(specificCommand);
            }
        }

        public abstract Task ExecuteSpecificCommand(ICommand command);
    }
}