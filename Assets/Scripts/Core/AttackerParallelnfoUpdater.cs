using Abstractions;
using UnityEngine;
using Zenject;

namespace Core
{
    public class AttackerParallelnfoUpdater : MonoBehaviour, ITickable, IMotor
    {
        [Inject] private IAutomaticAttacker _automaticAttacker;
        [Inject] private IHolderCommandExecutor _holderCommand;

        public void Tick()
        {
            AutoAttackEvaluator
                .AttackersDictionary
                .AddOrUpdate(gameObject, new AutoAttackEvaluator.AttackerParallelnfo(_automaticAttacker.VisionRadius, _holderCommand.CurrentCommand), (go, value) =>
{
    value.VisionRadius = _automaticAttacker.VisionRadius;
    value.CurrentCommand = _holderCommand.CurrentCommand;
    return value;
});
        }

        private void OnDestroy()
        {
            AutoAttackEvaluator.AttackersDictionary.TryRemove(gameObject, out _);
        }
    }
}