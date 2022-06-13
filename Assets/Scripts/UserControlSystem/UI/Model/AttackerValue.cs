using Abstractions;
using UnityEngine;


namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(AttackerValue), menuName = "Strategy Game/" + nameof(AttackerValue))]
    public sealed class AttackerValue : ScriptableValue<IAttackable>
    {
    }
}