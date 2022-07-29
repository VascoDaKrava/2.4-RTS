using System;
using System.Threading;
using Abstractions;
using UnityEngine;

namespace Core.CommandExecutors
{
    public partial class CommandAttackExecutor
    {
        public sealed partial class AttackOperation : IAwaitable<AsyncExtensions.Void>
        {
            private float _destDeviation = 0.9f;
            private int _threadSleepTime = 100;
            private bool _isCancelled;
            private readonly CommandAttackExecutor _attackCommandExecutor;
            private readonly IDamagable _target;

            private event Action OnComplete;

            public AttackOperation(CommandAttackExecutor attackCommandExecutor, IDamagable target)
            {
                _target = target;
                _attackCommandExecutor = attackCommandExecutor;

                var thread = new Thread(AttackAlgorythm);
                thread.Start();
            }

            public void Cancel()
            {
                _isCancelled = true;
                OnComplete?.Invoke();
            }

            private void AttackAlgorythm(object obj)
            {
                while (true)
                {
                    if (
                        _attackCommandExecutor == null
                        || _attackCommandExecutor._healthHolder.Health <= 0
                        || _target.Health <= 0
                        || _isCancelled
                        )
                    {
                        OnComplete?.Invoke();
                        return;
                    }

                    var targetPosition = default(Vector3);
                    var ourPosition = default(Vector3);
                    var ourRotation = default(Quaternion);

                    lock (_attackCommandExecutor)
                    {
                        targetPosition = _attackCommandExecutor._targetPosition;
                        ourPosition = _attackCommandExecutor._ourPosition;
                        ourRotation = _attackCommandExecutor._ourRotation;
                    }

                    var vector = targetPosition - ourPosition;
                    var distanceToTarget = vector.magnitude;

                    if (distanceToTarget > _attackCommandExecutor._attackingRange)
                    {
                        var finalDestination = targetPosition - vector.normalized * (_attackCommandExecutor._attackingRange * _destDeviation);
                        _attackCommandExecutor._targetPositions.OnNext(finalDestination);
                        Thread.Sleep(_threadSleepTime);
                    }
                    else if (ourRotation != Quaternion.LookRotation(vector))
                    {
                        _attackCommandExecutor._targetRotations.OnNext(Quaternion.LookRotation(vector));
                    }
                    else
                    {
                        _attackCommandExecutor._attackTargets.OnNext(_target);
                        Thread.Sleep(_attackCommandExecutor._attackingPeriod);
                    }
                }
            }

            public IAwaiter<AsyncExtensions.Void> GetAwaiter()
            {
                return new AttackOperationAwaiter(this);
            }
        }
    }
}