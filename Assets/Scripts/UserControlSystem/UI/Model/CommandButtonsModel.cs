using System;
using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UniRx;
using Zenject;

namespace UserControlSystem.UI.Model
{
    public sealed class CommandButtonsModel
    {
        public event Action<ICommandExecutor<ICommand>> OnCommandAccepted;
        public event Action OnCommandSent;
        public event Action OnCommandCancel;

        [Inject] private CommandCreatorBase<IProduceUnitCommand<UnitBase>> _unitProducer;
        [Inject] private CommandCreatorBase<IAttackCommand> _attacker;
        [Inject] private CommandCreatorBase<IStopCommand> _stopper;
        [Inject] private CommandCreatorBase<IMoveCommand> _mover;
        [Inject] private CommandCreatorBase<IPatrolCommand> _patroller;
        [Inject] private CommandCreatorBase<ITeleportCommand> _teleporter;
        [Inject] private CommandCreatorBase<ISetRallyPointCommand> _rallyPointSetter;

        private bool _commandIsPending;
        public Subject<bool> IsCommandPending = new Subject<bool>();

        public void OnCommandButtonClicked(ICommandExecutor<ICommand> commandExecutor, Type type)
        {
            if (_commandIsPending)
            {
                ProcessOnCancel();
            }

            _commandIsPending = true;
            IsCommandPending.OnNext(_commandIsPending);
            OnCommandAccepted?.Invoke(commandExecutor);

            _unitProducer.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command), type);

            _attacker.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command), type);
            _stopper.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command), type);
            _mover.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command), type);
            _patroller.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command), type);
            _teleporter.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command), type);
            _rallyPointSetter.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command), type);
        }

        public void ExecuteCommandWrapper(ICommandExecutor<ICommand> commandExecutor, ICommand command)
        {
            commandExecutor.TryExecuteCommand(command);
            _commandIsPending = false;
            IsCommandPending.OnNext(_commandIsPending);
            OnCommandSent?.Invoke();
        }

        public void OnSelectionChanged()
        {
            _commandIsPending = false;
            IsCommandPending.OnNext(_commandIsPending);
            ProcessOnCancel();
        }

        private void ProcessOnCancel()
        {
            _unitProducer.ProcessCancel();
            _attacker.ProcessCancel();
            _stopper.ProcessCancel();
            _mover.ProcessCancel();
            _patroller.ProcessCancel();
            _teleporter.ProcessCancel();
            _rallyPointSetter.ProcessCancel();

            OnCommandCancel?.Invoke();
        }
    }
}