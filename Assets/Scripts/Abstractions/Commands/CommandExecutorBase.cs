using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.Commands
{
    public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor<ICommand> where T : ICommand
    {
        private bool _currentCommandInWork = false;

        public async Task TryExecuteCommand(object command)
        {
            if (command is T specificCommand)
            {
                _currentCommandInWork = true;
                await ExecuteSpecificCommand(specificCommand);
                _currentCommandInWork = false;
            }
        }

        public abstract Task ExecuteSpecificCommand(ICommand command);
    }
}