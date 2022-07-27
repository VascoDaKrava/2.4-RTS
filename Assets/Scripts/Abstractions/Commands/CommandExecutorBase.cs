using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Abstractions.Commands
{
    public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor<ICommand> where T : ICommand
    {
        [Inject] private IHolderCommandExecutor _commandHolder;
        [SerializeField] private bool _currentCommandInWork = false;

        public async Task TryExecuteCommand(object command)
        {
            if (command is T specificCommand)
            {
                _currentCommandInWork = true;
                _commandHolder.CurrentCommand = specificCommand;
                await ExecuteSpecificCommand(specificCommand);
                _currentCommandInWork = false;
                _commandHolder.CurrentCommand = default;
            }
        }

        public abstract Task ExecuteSpecificCommand(ICommand command);
    }
}