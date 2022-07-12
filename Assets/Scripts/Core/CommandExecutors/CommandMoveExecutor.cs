using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading;
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
        private UnitCTSource _unitCTSource;

        private void Start()
        {
            _unitCTSource = GetComponent<UnitCTSource>();
        }

        public override async void ExecuteSpecificCommand(IMoveCommand command)
        {
            _agent.destination = command.Targets[0];
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
            }

            _unitCTSource.CTSource = null;
            _animator.SetTrigger(AnimationState.Idle);
        }
    }
}