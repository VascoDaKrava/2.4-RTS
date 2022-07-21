using Abstractions;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core
{
    public sealed class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>, IHolderUnitMovementStop
    {
        [Inject] private IHolderNavMeshAgent _agentHolder;

        private IDisposable _moveObserver;

        [SerializeField] private float _velocityStopFactor = 0.3f;
        [SerializeField] private int _framesCountStopFactor = 60;

        public event Action OnStop;

        private void Start()
        {
            Observable
                .EveryUpdate()
                .Where(_ => _agentHolder.NavMeshAgent.hasPath)
                .Where(_ => _agentHolder.NavMeshAgent.velocity.sqrMagnitude < _velocityStopFactor)
                .Select(_ => Time.frameCount)
                .Distinct()
                .Buffer(_framesCountStopFactor)
                .Subscribe(_ => DoStop())// When destination is unreachable
                .AddTo(this);
        }

        public void StartObseringMovement()
        {
            _moveObserver = Observable
                .EveryUpdate()
                .Where(_ => !_agentHolder.NavMeshAgent.hasPath)
                .Subscribe(_ => DoStop())// When path was finished
                .AddTo(this);
        }

        public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new StopAwaiter(this);

        public void DoStop()
        {
            _agentHolder.NavMeshAgent.isStopped = true;
            _agentHolder.NavMeshAgent.ResetPath();
            _moveObserver?.Dispose();
            OnStop?.Invoke();
        }
    }
}