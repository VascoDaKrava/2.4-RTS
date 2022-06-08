using System;
using System.Collections.Generic;
using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using UserControlSystem.CommandsRealization;
using UserControlSystem.UI.View;
using Utils;

namespace UserControlSystem.UI.Presenter
{
    public sealed class CommandButtonsPresenter : MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectable;
        [SerializeField] private CommandButtonsView _view;
        [SerializeField] private AssetsContext _context;

        private ISelectable _currentSelectable;

        private void Start()
        {
            _selectable.OnSelected += ONSelected;
            ONSelected(_selectable.CurrentValue);

            _view.Clear();
            _view.OnClick += ONButtonClick;
        }

        private void ONSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable)
            {
                return;
            }
            _currentSelectable = selectable;

            _view.Clear();
            if (selectable != null)
            {
                var commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());
                _view.MakeLayout(commandExecutors);
            }
        }

        private void ONButtonClick(ICommandExecutor commandExecutor)
        {
            switch (commandExecutor)
            {
                case CommandExecutorBase<IProduceUnitCommand> unitProducer:
                    unitProducer.ExecuteSpecificCommand(_context.Inject(new ProduceUnitCommand()));
                    break;

                case CommandExecutorBase<IAttackCommand> attackExecutor:
                    attackExecutor.ExecuteSpecificCommand(new AttackCommand());
                    break;

                case CommandExecutorBase<IMoveCommand> moveExecutor:
                    moveExecutor.ExecuteSpecificCommand(new MoveCommand());
                    break;

                case CommandExecutorBase<IPatrolCommand> patrolExecutor:
                    patrolExecutor.ExecuteSpecificCommand(new PatrolCommand());
                    break;

                case CommandExecutorBase<IStopCommand> stopExecutor:
                    stopExecutor.ExecuteSpecificCommand(new StopCommand());
                    break;

                default:
                    throw new ApplicationException($"{nameof(CommandButtonsPresenter)}.{nameof(ONButtonClick)}: " +
                                           $"Unknown type of commands executor: {commandExecutor.GetType().FullName}!");
                    break;
            }
        }
    }
}