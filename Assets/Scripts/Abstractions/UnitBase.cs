using UnityEngine;

namespace Abstractions
{
    public abstract class UnitBase : MonoBehaviour, ISelectable, IAttackable
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

        public Vector3 Position { get => transform.position; set => transform.position = value; }
    }
}