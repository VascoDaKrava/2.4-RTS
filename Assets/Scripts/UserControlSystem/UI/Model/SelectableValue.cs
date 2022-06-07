using System;
using Abstractions;
using UnityEngine;

namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(SelectableValue), menuName = "Strategy Game/" + nameof(SelectableValue), order = 0)]
    public class SelectableValue : ScriptableObject
    {
        public ISelectable CurrentValue { get; private set; }
        public Action<ISelectable> OnSelected;

        public void SetValue(ISelectable value)
        {
            //if (CurrentValue != null)
            //{
            //    CurrentValue.Selected = false;
            //}
            
            CurrentValue = value;

            //if (value != null)
            //{
            //    value.Selected = true;
            //}

            OnSelected?.Invoke(value);
        }
    }
}