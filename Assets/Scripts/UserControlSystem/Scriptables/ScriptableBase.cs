using Abstractions;
using System;
using UniRx;
using UnityEngine;

namespace UserControlSystem
{
    public abstract class ScriptableBase<T> : ScriptableObject, IAwaitable<T>, IObservable<T>
    {
        private readonly ReactiveProperty<T> _rValue = new();

        public T CurrentValue
        {
            get => _rValue.Value;
            private set => _rValue.Value = value;
        }

        public void SetValue(T value) => CurrentValue = value;

        public IAwaiter<T> GetAwaiter() => new NewValueNotifier<T>(this);

        public IDisposable Subscribe(IObserver<T> observer) => _rValue.Subscribe(observer);
    }
}