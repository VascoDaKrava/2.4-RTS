using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Abstractions.Commands
{
    public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor<ICommand> where T : ICommand
    {
        [Inject] private IHolderCommandExecutor _commandHolder;

        public async Task TryExecuteCommand(object command)
        {
            if (command is T specificCommand)
            {
                _commandHolder.CurrentCommand = specificCommand;
                await ExecuteSpecificCommand(specificCommand);
                _commandHolder.CurrentCommand = default;
            }
        }

        public abstract Task ExecuteSpecificCommand(ICommand command);
    }
}