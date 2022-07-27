using Abstractions;
using Abstractions.Commands;
using UnityEngine;
using Zenject;

namespace Core
{
    public class AttackerParallelnfoUpdater : MonoBehaviour, ITickable
    {
        [Inject] private IAutomaticAttacker _automaticAttacker;

        public void Tick()
        {
            AutoAttackEvaluator
                .AttackersDictionary
                .AddOrUpdate(gameObject, new AutoAttackEvaluator.AttackerParallelnfo(_automaticAttacker.VisionRadius, default/*_queue.CurrentCommand*/), (go, value) =>
{
    value.VisionRadius = _automaticAttacker.VisionRadius;
    value.CurrentCommand = default/*_queue.CurrentCommand*/;
    return value;
});
        }

        private void OnDestroy()
        {
            AutoAttackEvaluator.AttackersDictionary.TryRemove(gameObject, out _);
        }
    }
}