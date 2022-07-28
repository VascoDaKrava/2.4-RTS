using Abstractions;
using Abstractions.Commands;
using UnityEngine;
using UnityEngine.AI;
using UserControlSystem;
using Zenject;

namespace Core
{
    public sealed class Tower : MonoBehaviour, ISelectable, IDamagable, IAttacker, IHolderCommandExecutor, IHolderAnimator
        ,IHolderNavMeshAgent ,IHolderUnitMovementStop
    {
        [Space]
        [SerializeField] private Sprite _icon;

        [SerializeField] private float _maxHealth = 200;
        [SerializeField] private float _health = 100;

        [Space]
        [SerializeField] private GameObject _selectionMarker;
        [SerializeField] private Animator _animator;

        [Space]
        [SerializeField] private float _attackStrength = 50.0f;
        [SerializeField] private float _attackRange = 10.0f;
        [SerializeField] private int _attackPeriod = 1000;

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

        public float AttackStrength => _attackStrength;
        public float AttackRange => _attackRange;
        public int AttackPeriod => _attackPeriod;

        public Animator Animator => _animator;

        public NavMeshAgent NavMeshAgent => throw new System.NotImplementedException();

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

        public void StartObservingMovement()
        {
            throw new System.NotImplementedException();
        }

        public void DoStop()
        {
            throw new System.NotImplementedException();
        }
    }
}