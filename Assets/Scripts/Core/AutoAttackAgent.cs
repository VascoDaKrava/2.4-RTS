using Abstractions;
using Core.CommandExecutors;
using UniRx;
using UnityEngine;
using UserControlSystem.CommandsRealization;

namespace Core
{
    public class AutoAttackAgent : MonoBehaviour
    {
        [SerializeField] private CommandAttackExecutor _attackExecutor;

        private void Start()
        {
            AutoAttackEvaluator.AutoAttackCommands
                .ObserveOnMainThread()
                .Where(command => command.Attacker == gameObject)
                .Where(command => command.Attacker != null && command.Target != null)
                .Subscribe(command => AutoAttack(command.Target))
                .AddTo(this);
        }

        private async void AutoAttack(GameObject target)
        {
            Debug.Log($"Start autoattack {target}");
            await _attackExecutor.TryExecuteCommand(new AutoAttackCommand(target.GetComponent<IDamagable>()));
            Debug.Log("Finish autoattack");
        }
    }
}