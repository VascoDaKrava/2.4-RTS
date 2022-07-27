using Abstractions;
using UnityEngine;
using Zenject;

namespace Core
{
    public class FactionMemberParallelInfoUpdater : MonoBehaviour, ITickable
    {
        [Inject] private IFactionMember _factionMember;

        public void Tick()
        {
            AutoAttackEvaluator
                .FactionMembersDictionary
                .AddOrUpdate(gameObject, new AutoAttackEvaluator.FactionMemberParallelInfo(transform.position, _factionMember.FactionID)
                , (go, value) =>
                {
                    value.Position = transform.position;
                    value.Faction = _factionMember.FactionID;
                    return value;
                });
        }

        private void OnDestroy()
        {
            AutoAttackEvaluator.FactionMembersDictionary.TryRemove(gameObject, out _);
        }
    }
}