using Abstractions;
using UnityEngine;

namespace Core
{
    public sealed class MainBuilding : MonoBehaviour, ISelectable, IDamagable, IHolderHealth
    {
        [SerializeField] private float _maxHealth = 1000;
        [SerializeField] private Sprite _icon;

        [SerializeField] private GameObject _selectionMarker;

        private float _health = 1000;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;

        public bool Selected
        {
            get => _selectionMarker.activeSelf;
            set => _selectionMarker.SetActive(value);
        }

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public void GetDamage(float value)
        {
            _health -= value;
            Debug.Log($"{this} get {value} damage.");
        }
    }
}