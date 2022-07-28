using Abstractions;
using Core.CommandExecutors;
using UniRx;
using UnityEngine;
using UserControlSystem;
using UserControlSystem.CommandsRealization;
using Zenject;

namespace Core
{
    public class AutoMover : MonoBehaviour, IMotor
    {
        [Inject] ICommandButtonsPresenter _commandButtons;
        [Inject] private CommandMoveExecutor _moveExecutor;
        [Inject] private Vector3Value _groundPointClick;
        [Inject] private ISelectable _selectable;

        [SerializeField] private bool _isOtherCommandWaitInput = false;
        [SerializeField] private bool _isInputPending = false;

        private void Start()
        {
            _commandButtons.IsCommandPending
                .Subscribe(state => _isOtherCommandWaitInput = state)
                .AddTo(this);

            _groundPointClick
                .Where(x => x != default)
                .Where(_ => _selectable.Selected)
                .Subscribe(target => CanMove(target))
                .AddTo(this);
        }

        private async void CanMove(Vector3 target)
        {
            if (_isOtherCommandWaitInput)
            {
                return;
            }

            await _moveExecutor.TryExecuteCommand(new MoveCommand(target));
        }
    }
}