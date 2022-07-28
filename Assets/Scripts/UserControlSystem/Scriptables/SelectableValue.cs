using Abstractions;
using UnityEngine;


namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(SelectableValue), menuName = "RTS/SO_value/" + nameof(SelectableValue))]
    public sealed class SelectableValue : ScriptableBase<ISelectable>
    {
    }
}