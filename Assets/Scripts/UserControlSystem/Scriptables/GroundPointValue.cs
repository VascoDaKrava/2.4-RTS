using UnityEngine;


namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(GroundPointValue), menuName = "RTS/SO_value/" + nameof(GroundPointValue))]
    public sealed class GroundPointValue : ScriptableBase<Vector3>
    {
    }
}