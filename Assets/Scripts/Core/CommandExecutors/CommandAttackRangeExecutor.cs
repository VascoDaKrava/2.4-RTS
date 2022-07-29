using System.Threading.Tasks;
using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.CommandExecutors
{
    public sealed class CommandAttackRangeExecutor : CommandExecutorBase<IAttackCommand>
    {
        [Inject] private UnitCTSource _unitCTSource;

        [Inject] private IAttacker _attacker;
        [Inject] private IHolderAnimator _animatorHolder;

        private Transform _targetTransform;

        public async override Task ExecuteSpecificCommand(ICommand commandBase)
        {
            var command = (IAttackCommand)commandBase;
            _targetTransform = (command.Target as Component).transform;

            var distance = Vector3.Distance(_targetTransform.position, transform.position);
            if (distance <= _attacker.AttackRange)
            {
                Debug.Log($"Distance : {distance}/{_attacker.AttackRange}");
                await StartAttack();
            }
            else
            {
                Debug.Log($"Out of range : {distance}/{_attacker.AttackRange}");
            }
        }

        private async Task StartAttack()
        {
            var attackProcess = new AttackProcess();

            try
            {
                await attackProcess.WithCancellation(_unitCTSource.Token);
            }
            catch
            {
                Debug.Log("Attack cancel");
            }

            Debug.Log("Attack finish");
        }

        private class AttackProcess : IAwaitable<AsyncExtensions.Void>
        {
            public AttackProcess()
            {
                Debug.Log("Attack start");
            }

            public IAwaiter<AsyncExtensions.Void> GetAwaiter()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}