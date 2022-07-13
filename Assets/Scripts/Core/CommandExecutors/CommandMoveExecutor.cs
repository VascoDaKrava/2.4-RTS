using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using AnimationState = Abstractions.AnimationState;

namespace Core.CommandExecutors
{
    public sealed class CommandMoveExecutor : CommandExecutorBase<IMoveCommand>
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private UnitCTSource _unitCTSource;
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
            _agent.destination = target;
            _animator.SetTrigger(AnimationState.Walk);
            _unitCTSource.CTSource = new CancellationTokenSource();

            try
            {
                await _stop.WithCancellation(_unitCTSource.CTSource.Token);
            }
            catch
            {
                _agent.isStopped = true;
                _agent.ResetPath();
                _rqueueMovePoints.Clear();
            }

            _unitCTSource.CTSource = null;
            _animator.SetTrigger(AnimationState.Idle);

            if (_rqueueMovePoints.Count > 0)
            {
                _rqueueMovePoints.RemoveAt(0);
            }
        }
    }
}