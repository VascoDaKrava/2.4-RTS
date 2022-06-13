using Abstractions;
using UnityEngine;


namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(AttackerValue), menuName = "RTS/SO_value/" + nameof(AttackerValue))]
    public sealed class AttackerValue : ScriptableValue<IAttackable>
    {
    }
}