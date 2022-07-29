using System;
using Abstractions.Commands;


namespace UserControlSystem
{
    public abstract class CommandCreatorBase<T> where T : ICommand
    {
        public ICommandExecutor<ICommand> ProcessCommandExecutor(ICommandExecutor<ICommand> commandExecutor, Action<T> callback, Type type)
        {
            var classSpecificExecutor = commandExecutor as CommandExecutorBase<T>;
            if (classSpecificExecutor != null)
            {
                ClassSpecificCommandCreation(callback, type);
            }
            return commandExecutor;
        }

        protected abstract void ClassSpecificCommandCreation(Action<T> creationCallback, Type type);

        public virtual void ProcessCancel() { }
    }
}