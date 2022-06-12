using System;
using UnityEngine;


namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(T), menuName = "Strategy Game/" + nameof(T), order = 0)]
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