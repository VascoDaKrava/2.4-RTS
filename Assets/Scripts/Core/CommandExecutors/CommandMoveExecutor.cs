using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading.Tasks;
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
        [Inject] private UnitMovementStop _unitMoveStop;
        [Inject] private UnitCTSource _unitCTSource;

        private ReactiveCollection<Vector3> _rqueueMovePoints;

        public override async Task ExecuteSpecificCommand(ICommand baseCommand)
        {
            var command = (IMoveCommand)baseCommand;
            _rqueueMovePoints = command.Targets.ToReactiveCollection();

            _rqueueMovePoints
                .ObserveCountChanged()
                .Subscribe(async count => await OnObserveCountChangedAsync(count))
                .AddTo(this);

            await DoMove(_rqueueMovePoints[0]);
        }

        private async Task OnObserveCountChangedAsync(int count)
        {
            if (count > 0)
            {
                await DoMove(_rqueueMovePoints[0]);
            }
        }

        private async Task DoMove(Vector3 target)
        {
            _agentHolder.NavMeshAgent.destination = target;
            _stopMoveHolder.StartObservingMovement();
            _animatorHolder.Animator.SetTrigger(AnimatorParams.Walk);
            _unitCTSource.NewToken();

            try
            {
                Debug.Log("try MoveExec");
                await _unitMoveStop.WithCancellation(_unitCTSource.Token);
            }
            catch
            {
                Debug.Log("catch MoveExec");
                _rqueueMovePoints.Clear();
                _stopMoveHolder.DoStop();
            }

            if (_rqueueMovePoints.Count > 0)
            {
                _rqueueMovePoints.RemoveAt(0);
            }
        }
    }
}