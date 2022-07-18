using Abstractions;
using UnityEngine;

namespace Core
{
    public sealed class MainBuilding : MonoBehaviour, ISelectable, IDamagable, IHolderHealth, IHolderRallyPoint
    {
        [SerializeField] private Sprite _icon;

        [SerializeField] private float _maxHealth = 1000;
        [SerializeField] private float _health = 1000;

        [SerializeField] private GameObject _selectionMarker;

        [SerializeField] private Transform _rallyPoint;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;

        public bool Selected
        {
            get => _selectionMarker.activeSelf;
            set
            {
                _selectionMarker.SetActive(value);
                _rallyPoint.gameObject.SetActive(value);
            }
        }

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Vector3 RallyPoint { get => _rallyPoint.position; set => _rallyPoint.position = value; }

        public void GetDamage(float value)
        {
            _health -= value;
            Debug.Log($"{this} get {value} damage.");
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}