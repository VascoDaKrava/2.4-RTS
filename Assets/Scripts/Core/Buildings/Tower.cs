using Abstractions;
using Abstractions.Commands;
using UnityEngine;
using UserControlSystem;
using Zenject;

namespace Core
{
    public sealed class Tower : MonoBehaviour, ISelectable, IDamagable
    {
        [SerializeField] private Sprite _icon;

        [SerializeField] private float _maxHealth = 200;
        [SerializeField] private float _health = 100;

        [SerializeField] private GameObject _selectionMarker;

        [Inject] private SelectableValue _selectedObject;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;

        public bool Selected
        {
            get => _selectionMarker.activeSelf;
            set
            {
                _selectionMarker.SetActive(value);
            }
        }

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public ICommand CurrentCommand { get => default; set { } }

        public void BeforeDestroy()
        {
            if (Selected)
            {
                _selectedObject.SetValue(null);
            }
        }

        private void OnDestroy()
        {
            BeforeDestroy();
        }

        public void GetDamage(float value)
        {
            _health -= value;
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}