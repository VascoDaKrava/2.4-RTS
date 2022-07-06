using Abstractions;
using UnityEngine;


namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(DamagableValue), menuName = "RTS/SO_value/" + nameof(DamagableValue))]
    public sealed class DamagableValue : ScriptableBase<IDamagable>
    {
    }
}