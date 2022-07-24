using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using Core.CommandExecutors;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UserControlSystem.CommandsRealization;

namespace Core
{
    public sealed class Human : UnitBase, IProduceUnitCommand
    {
        [SerializeField] private float _timeForDestroyAfterDie = 3.0f;

        [Space]
        [SerializeField] private float _attackStrength = 25.0f;
        [SerializeField] private float _attackRange = 2.0f;
        [SerializeField] private int _attackPeriod = 1000;

        [Space]
        [SerializeField] private float _maxHealth = 100.0f;
        [SerializeField] private float _health = 100.0f;
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _selectionMarker;
        [SerializeField] private CommandStopExecutor _commandStopExecutor;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        [Space]
        [SerializeField] private float _productionTime = 3.0f;
        [SerializeField] private string _unitName = "Human";

        public override float AttackStrength => _attackStrength;
        public override float AttackRange => _attackRange;
        public override int AttackPeriod => _attackPeriod;

        public override float MaxHealth => _maxHealth;
        public override float Health => _health;
        public override Sprite Icon => _icon;
        public override GameObject SelectionMarker => _selectionMarker;

        public float ProductionTime => _productionTime;
        public GameObject UnitPrefab => gameObject;
        public string Name => _unitName;

        public override NavMeshAgent NavMeshAgent => _navMeshAgent;

        public override Animator Animator => _animator;

        public override void GetDamage(float value)
        {
            _health -= value;
            if (_health <= 0)
            {
                DieAsync();
            }
        }

        private async void DieAsync()
        {
            await _commandStopExecutor.ExecuteSpecificCommand(new StopCommand());
            _animator.SetTrigger(AnimatorParams.Die);

            var animationInfo = _animator.GetCurrentAnimatorStateInfo(0);
            var startHash = animationInfo.shortNameHash;

            while (animationInfo.shortNameHash == AnimatorParams.DieStateInt
                || _animator.GetCurrentAnimatorStateInfo(0).shortNameHash == startHash)
            {
                await Task.Yield();
            }

            Destroy(gameObject, _timeForDestroyAfterDie);
        }
    }
}
