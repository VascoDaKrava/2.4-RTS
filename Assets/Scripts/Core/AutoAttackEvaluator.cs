using System.Collections.Concurrent;
using System.Threading.Tasks;
using Abstractions.Commands.CommandsInterfaces;
using UniRx;
using UnityEngine;

namespace Core
{
    public sealed partial class AutoAttackEvaluator : MonoBehaviour
    {
        public static ConcurrentDictionary<GameObject, AttackerParallelnfo> AttackersDictionary =
            new ConcurrentDictionary<GameObject, AttackerParallelnfo>();

        public static ConcurrentDictionary<GameObject, FactionMemberParallelInfo> FactionMembersDictionary =
            new ConcurrentDictionary<GameObject, FactionMemberParallelInfo>();

        public static Subject<Command> AutoAttackCommands = new Subject<Command>();

        private void Update()
        {
            Parallel.ForEach(AttackersDictionary, kvp => Evaluate(kvp.Key, kvp.Value));
        }

        private void Evaluate(GameObject attacker, AttackerParallelnfo attackerInfo)
        {
            if (attackerInfo.CurrentCommand is IMoveCommand)
            {
                return;
            }

            if (attackerInfo.CurrentCommand is IAttackCommand
                && !(attackerInfo.CurrentCommand is Command))
            {
                return;
            }

            var attackerFactionInfo = default(FactionMemberParallelInfo);

            if (!FactionMembersDictionary.TryGetValue(attacker, out attackerFactionInfo))
            {
                return;
            }

            foreach (var (targetGo, targetFactionInfo) in FactionMembersDictionary)
            {
                if (attackerFactionInfo.Faction == targetFactionInfo.Faction)
                {
                    continue;
                }

                var distance = Vector3.Distance(attackerFactionInfo.Position, targetFactionInfo.Position);

                if (distance > attackerInfo.VisionRadius)
                {
                    continue;
                }

                AutoAttackCommands.OnNext(new Command(attacker, targetGo));

                break;
            }
        }
    }
}