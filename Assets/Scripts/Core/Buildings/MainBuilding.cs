using Abstractions;
using Abstractions.Commands;
using UnityEngine;
using UserControlSystem;
using Zenject;

namespace Core
{
    public sealed class MainBuilding : MonoBehaviour, ISelectable, IDamagable, IHolderRallyPoint, IHolderCommandExecutor
    {
        [SerializeField] private Sprite _icon;

        [SerializeField] private float _maxHealth = 1000;
        [SerializeField] private float _health = 1000;

        [SerializeField] private GameObject _selectionMarker;

        [SerializeField] private Transform _rallyPoint;

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
                _rallyPoint.gameObject.SetActive(value);
            }
        }

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Vector3 RallyPoint { get => _rallyPoint.position; set => _rallyPoint.position = value; }

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
            Debug.Log($"{this} get {value} damage.");
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}