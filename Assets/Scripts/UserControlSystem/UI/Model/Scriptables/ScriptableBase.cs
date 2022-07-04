using Abstractions;
using System;
using UniRx;
using UnityEngine;


namespace UserControlSystem
{
    public abstract class ScriptableBase<T> : ScriptableObject, IAwaitable<T>, IObservable<T>
    {
        private ReactiveProperty<T> _rValue = new ReactiveProperty<T>();

        //public T CurrentValue { get; private set; }
        public T CurrentValue
        {
            get => _rValue.Value;
            private set => _rValue.Value = value;
        }

        //public Action<T> OnNewValue;

        public void SetValue(T value)
        {
            //_rValue.Value = value;
            CurrentValue = value;
            //OnNewValue?.Invoke(value);
        }

        public IAwaiter<T> GetAwaiter()
        {
            return new NewValueNotifier<T>(this);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _rValue.Subscribe(observer);
        }
    }
}