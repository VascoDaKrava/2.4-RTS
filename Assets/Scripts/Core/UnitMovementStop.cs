using Abstractions;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    public class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _velocityStopFactor = 0.3f;
        [SerializeField] private int _framesCountStopFactor = 60;

        public event Action OnStop;

        private void Start()
        {
            Observable
                .EveryUpdate()
                .Where(velocity => _agent.velocity.sqrMagnitude < _velocityStopFactor)
                .Select(_ => Time.frameCount)
                .Distinct()
                .Buffer(_framesCountStopFactor)
                .Subscribe(_ => DoStop())
                .AddTo(this);
        }

        private void Update()
        {
            if (!_agent.pathPending)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                    {
                        DoStop();
                    }
                }
            }
        }

        public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new StopAwaiter(this);

        private void DoStop()
        {
            _agent.isStopped = true;
            _agent.ResetPath();
            OnStop?.Invoke();
        }
    }
}