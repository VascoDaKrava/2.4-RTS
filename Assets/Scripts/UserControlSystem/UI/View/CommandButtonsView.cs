using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UserControlSystem.UI.View
{
    public sealed class CommandButtonsView : MonoBehaviour
    {
        public Action<ICommandExecutor<ICommand>, Type> OnClick;

        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _patrolButton;
        [SerializeField] private Button _stopButton;
        [SerializeField] private Button _produceUnitHumanButton;
        [SerializeField] private Button _produceUnitSkeletonButton;
        [SerializeField] private Button _setRallyPointButton;

        private Dictionary<Type, Button> _buttonsByExecutorType;

        private void Awake()
        {
            _buttonsByExecutorType = new Dictionary<Type, Button>();

            _buttonsByExecutorType
                .Add(typeof(CommandExecutorBase<IAttackCommand>), _attackButton);

            _buttonsByExecutorType
                .Add(typeof(CommandExecutorBase<IMoveCommand>), _moveButton);

            _buttonsByExecutorType
                .Add(typeof(CommandExecutorBase<IPatrolCommand>), _patrolButton);

            _buttonsByExecutorType
                .Add(typeof(CommandExecutorBase<IStopCommand>), _stopButton);

            _buttonsByExecutorType
                .Add(typeof(CommandExecutorBase<IProduceUnitCommand<Human>>), _produceUnitHumanButton);

            _buttonsByExecutorType
                .Add(typeof(CommandExecutorBase<IProduceUnitCommand<Skeleton>>), _produceUnitSkeletonButton);

            _buttonsByExecutorType
                .Add(typeof(CommandExecutorBase<ISetRallyPointCommand>), _setRallyPointButton);
        }

        public void BlockInteractions(ICommandExecutor<ICommand> comExec)
        {
            UnblockAllInteractions();
            if (comExec is CommandExecutorBase<IProduceUnitCommand<UnitBase>>)
            {
                _produceUnitHumanButton.interactable = false;
                _produceUnitSkeletonButton.interactable = false;
                return;
            }
            GETButtonGameObjectByType(comExec.GetType()).interactable = false;
        }

        public void UnblockAllInteractions() => SetInteractible(true);

        private void SetInteractible(bool value)
        {
            _attackButton.GetComponent<Selectable>().interactable = value;
            _moveButton.GetComponent<Selectable>().interactable = value;
            _patrolButton.GetComponent<Selectable>().interactable = value;
            _stopButton.GetComponent<Selectable>().interactable = value;

            _produceUnitHumanButton.GetComponent<Selectable>().interactable = value;
            _produceUnitSkeletonButton.GetComponent<Selectable>().interactable = value;
            _setRallyPointButton.GetComponent<Selectable>().interactable = value;
        }

        public void MakeLayout(IEnumerable<ICommandExecutor<ICommand>> commandExecutors)
        {
            foreach (var currentExecutor in commandExecutors)
            {
                if (currentExecutor is CommandExecutorBase<IProduceUnitCommand<UnitBase>>)
                {
                    _produceUnitHumanButton.gameObject.SetActive(true);
                    _produceUnitHumanButton.onClick.AddListener(() => OnClick?.Invoke(currentExecutor, typeof(Human)));

                    _produceUnitSkeletonButton.gameObject.SetActive(true);
                    _produceUnitSkeletonButton.onClick.AddListener(() => OnClick?.Invoke(currentExecutor, typeof(Skeleton)));

                    continue;
                }

                var button = GETButtonGameObjectByType(currentExecutor.GetType());
                button.gameObject.SetActive(true);
                button.onClick.AddListener(() => OnClick?.Invoke(currentExecutor, default));
            }
        }

        private Button GETButtonGameObjectByType(Type executorInstanceType)
        {
            return _buttonsByExecutorType
                .First(type => type.Key.IsAssignableFrom(executorInstanceType))
                .Value;
        }

        public void Clear()
        {
            foreach (var kvp in _buttonsByExecutorType)
            {
                kvp.Value.onClick.RemoveAllListeners();
                kvp.Value.gameObject.SetActive(false);
            }
        }
    }
}