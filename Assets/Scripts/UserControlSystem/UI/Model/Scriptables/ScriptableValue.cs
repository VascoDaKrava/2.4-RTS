using System;
using UnityEngine;


namespace UserControlSystem
{
    public abstract class ScriptableValue<T> : ScriptableObject
    {
        public T CurrentValue { get; private set; }

        public Action<T> OnValueChange;

        public void SetValue(T value)
        {
            CurrentValue = value;
            OnValueChange?.Invoke(value);
        }
    }
}