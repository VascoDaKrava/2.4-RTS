using Abstractions;
using UnityEngine;


namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(DamagableValue), menuName = "Strategy Game/" + nameof(DamagableValue))]
    public sealed class DamagableValue : ScriptableValue<IDamagable>
    {
    }
}