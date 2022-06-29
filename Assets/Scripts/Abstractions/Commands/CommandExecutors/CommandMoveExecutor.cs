using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using UnityEngine.AI;


namespace Abstractions.Commands.CommandExecutors
{
    public sealed class CommandMoveExecutor : CommandExecutorBase<IMoveCommand>
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        [SerializeField] private UnitMovementStop _stop;

        public override async void ExecuteSpecificCommand(IMoveCommand command)
        {
            Debug.Log($"{name} move to {command.Target}");
            _agent.destination = command.Target;
            _animator.SetTrigger(AnimationState.Walk.ToString());
            await _stop;
            _animator.SetTrigger(AnimationState.Idle.ToString());
        }
    }
}