using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.CommandExecutors
{
    public sealed class CommandMoveExecutor : CommandExecutorBase<IMoveCommand>
    {
        [Inject] private IHolderNavMeshAgent _agentHolder;
        [Inject] private IHolderAnimator _animatorHolder;
        [Inject] private IHolderUnitMovementStop _stopMoveHolder;
        [Inject] private UnitMovementStop _stop;
        [Inject] private UnitCTSource _unitCTSource;

        private ReactiveCollection<Vector3> _rqueueMovePoints;

        public override void ExecuteSpecificCommand(IMoveCommand command)
        {
            _rqueueMovePoints = command.Targets.ToReactiveCollection();
            _rqueueMovePoints.ObserveCountChanged().Subscribe(count => OnObserveCountChanged(count)).AddTo(this);
            DoMove(_rqueueMovePoints[0]);
        }

        private void OnObserveCountChanged(int count)
        {
            if (count > 0)
            {
                DoMove(_rqueueMovePoints[0]);
            }
        }

        private async void DoMove(Vector3 target)
        {
            _agentHolder.NavMeshAgent.destination = target;
            _stopMoveHolder.StartObseringMovement();
            _animatorHolder.Animator.SetTrigger(AnimatorParams.Walk);
            _unitCTSource.NewToken();

            try
            {
                await _stop.WithCancellation(_unitCTSource.Token);
            }
            catch
            {
                _rqueueMovePoints.Clear();
                _stopMoveHolder.DoStop();
            }

            _unitCTSource.ClearToken();
            _animatorHolder.Animator.SetTrigger(AnimatorParams.Idle);

            if (_rqueueMovePoints.Count > 0)
            {
                _rqueueMovePoints.RemoveAt(0);
            }
        }
    }
}