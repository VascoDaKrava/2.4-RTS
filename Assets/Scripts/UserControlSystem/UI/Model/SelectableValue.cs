using Abstractions;
using UnityEngine;


namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(SelectableValue), menuName = "Strategy Game/" + nameof(SelectableValue))]
    public sealed class SelectableValue : ScriptableValue<ISelectable>
    {
    }
}