using UnityEngine;
using UnityEngine.AI;

namespace Abstractions
{
    public abstract class UnitBase : MonoBehaviour, ISelectable, IAttacker, IDamagable, IHolderNavMeshAgent, IHolderAnimator
    {
        public abstract GameObject SelectionMarker { get; }

        public abstract float Health { get; }

        public abstract float MaxHealth { get; }

        public abstract Sprite Icon { get; }

        public bool Selected
        {
            get => SelectionMarker.activeSelf;
            set => SelectionMarker.SetActive(value);
        }

        public abstract float AttackStrength { get; }

        public abstract float AttackRange { get; }

        public abstract int AttackPeriod { get; }

        public Vector3 Position { get => transform.position; set => transform.position = value; }

        public abstract NavMeshAgent NavMeshAgent { get; }

        public abstract Animator Animator { get; }

        public abstract void GetDamage(float value);
    }
}