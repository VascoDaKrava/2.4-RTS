using Abstractions.Enums;
using UnityEngine;

namespace Core
{
    public sealed partial class AutoAttackEvaluator
    {
        public class FactionMemberParallelInfo
        {
            public Vector3 Position;
            public FactionID Faction;

            public FactionMemberParallelInfo(Vector3 position, FactionID faction)
            {
                Position = position;
                Faction = faction;
            }
        }
    }
}