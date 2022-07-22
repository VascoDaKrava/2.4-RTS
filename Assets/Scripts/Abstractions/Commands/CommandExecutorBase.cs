using UnityEngine;

namespace Abstractions.Commands
{
    public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor<ICommand>
    //public abstract class CommandExecutorBase<ICommand> : MonoBehaviour, ICommandExecutorLabel2
    {
        //public void ExecuteCommand(object command)
        //{
        //    ExecuteSpecificCommand((ICommand)command);
        //}

        //public abstract void ExecuteSpecificCommand(ICommand command);
        public abstract void ExecuteSpecificCommand(ICommand command);

        //public async Task TryExecuteCommand(object command)
        //{
        //    var specificCommand = command as T;
        //    if (specificCommand != null)
        //    {
        //        await ExecuteSpecificCommand(specificCommand);
        //    }
        //}

        //public abstract Task ExecuteSpecificCommand(T command);

    }
}