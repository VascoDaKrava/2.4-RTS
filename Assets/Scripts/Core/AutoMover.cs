using Abstractions;
using Core.CommandExecutors;
using UniRx;
using UnityEngine;
using UserControlSystem;
using UserControlSystem.CommandsRealization;
using Zenject;

namespace Core
{
    public class AutoMover : MonoBehaviour
    {
        [Inject] ICommandButtonsPresenter _commandButtons;
        [Inject] private CommandMoveExecutor _moveExecutor;
        [Inject] private UnitCTSource _unitCTSource;
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
                .Subscribe(target => CanMove(target));
        }

        private async void CanMove(Vector3 target)
        {
            Debug.Log("TryMove");

            if (_isOtherCommandWaitInput) return;

            Debug.Log("CanMove");

            Debug.Log("Go to " + target);

            //return;
            if (_unitCTSource.Token != default)
            {
                _unitCTSource.Cancel();
            }

            await _moveExecutor.TryExecuteCommand(new MoveCommand(target));

            Debug.Log("After await");
        }
    }
}