using System;
using System.Threading.Tasks;
using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.CommandExecutors
{
    public partial class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        [SerializeField] private bool _checkInject = false;

        [SerializeField] private CommandStopExecutor _commandStopExecutor;
        [Inject] private UnitCTSource _unitCTSource;

        [Inject] private IAttacker _attacker;
        [Inject] private IHolderAnimator _animatorHolder;
        [Inject] private IHolderHealth _healthHolder;
        [Inject] private IHolderNavMeshAgent _navMeshAgentHolder;

        [Inject(Id = "AttackRange")] private float _attackingRange;
        [Inject(Id = "AttackPeriod")] private int _attackingPeriod;

        private int _roundDigits = 2;

        private Vector3 _ourPosition;
        private Vector3 _targetPosition;
        private Quaternion _ourRotation;

        private readonly Subject<Vector3> _targetPositions = new Subject<Vector3>();
        private readonly Subject<Quaternion> _targetRotations = new Subject<Quaternion>();
        private readonly Subject<IDamagable> _attackTargets = new Subject<IDamagable>();

        private Transform _targetTransform;
        private AttackOperation _currentAttackOp;

        [Inject]
        private void Init()
        {
            _targetPositions
                .Select(value =>
                    new Vector3(
                        (float)Math.Round(value.x, _roundDigits),
                        (float)Math.Round(value.y, _roundDigits),
                        (float)Math.Round(value.z, _roundDigits)
                    ))
                .Distinct()
                .ObserveOnMainThread()
                .Subscribe(StartMovingToPosition);

            _attackTargets
                .ObserveOnMainThread()
                .Subscribe(StartAttackingTargets);

            _targetRotations
                .ObserveOnMainThread()
                .Subscribe(SetAttackRotation);
        }

        private void SetAttackRotation(Quaternion targetRotation)
        {
            transform.rotation = targetRotation;
        }

        private void StartAttackingTargets(IDamagable target)
        {
            //GetComponent<NavMeshAgent>().isStopped = true;
            //GetComponent<NavMeshAgent>().ResetPath();
            _navMeshAgentHolder.NavMeshAgent.isStopped = true;
            _navMeshAgentHolder.NavMeshAgent.ResetPath();
            _animatorHolder.Animator.SetTrigger(AnimatorParams.Attack);
            target.GetDamage(_attacker.AttackStrength);
            //target.ReceiveDamage(GetComponent<IDamageDealer>().Damage);
        }

        private void StartMovingToPosition(Vector3 position)
        {
            _navMeshAgentHolder.NavMeshAgent.destination = position;
            _animatorHolder.Animator.SetTrigger(AnimatorParams.Walk);
        }

        //public override async Task ExecuteSpecificCommand(IAttackCommand command)
        public async Task ExecuteSpecificCommand12(IAttackCommand command)
        {
            _targetTransform = (command.Target as Component).transform;
            _currentAttackOp = new AttackOperation(this, command.Target);
            Update();
            _unitCTSource.NewToken();// = new System.Threading.CancellationTokenSource();

            try
            {
                await _currentAttackOp.WithCancellation(_unitCTSource.Token);
            }
            catch
            {
                _currentAttackOp.Cancel();
            }
            
            _animatorHolder.Animator.SetTrigger(AnimatorParams.Idle);
            _currentAttackOp = null;
            _targetTransform = null;
            _unitCTSource.ClearToken();
        }

        private void Update()
        {
            if (_checkInject)
            {
                Debug.Log("AStrength = " + _attacker.AttackStrength);
                _checkInject = false;
            }

            if (_currentAttackOp == null)
            {
                return;
            }

            lock (this)
            {
                _ourPosition = transform.position;
                _ourRotation = transform.rotation;
                if (_targetTransform != null)
                {
                    _targetPosition = _targetTransform.position;
                }
            }
        }

        public override void ExecuteSpecificCommand(IAttackCommand command)
        {
            throw new NotImplementedException();
        }
    }
}