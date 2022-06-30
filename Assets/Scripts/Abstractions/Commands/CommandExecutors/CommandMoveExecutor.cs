using Abstractions.Commands.CommandsInterfaces;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Abstractions.Commands.CommandExecutors
{
    public sealed class CommandMoveExecutor : CommandExecutorBase<IMoveCommand>
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        [SerializeField] private UnitMovementStop _stop;
        [Inject] private CancellationTokenSource _ctSource;

        public override async void ExecuteSpecificCommand(IMoveCommand command)
        {
            Debug.Log($"{name} move to {command.Target}");
            _agent.destination = command.Target;
            _animator.SetTrigger(AnimationState.Walk);
            await _stop;
            //try
            //{
            //    await _stop.WithCancellation(_ctSource.Token);
            //}
            //catch
            //{
            //    _agent.isStopped = true;
            //    _agent.ResetPath();
            //}

            _ctSource = null;
            _animator.SetTrigger(AnimationState.Idle);
        }
    }
}