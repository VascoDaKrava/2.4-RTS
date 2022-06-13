using UnityEngine;


namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(Vector3Value), menuName = "Strategy Game/" + nameof(Vector3Value))]
    public sealed class Vector3Value : ScriptableValue<Vector3>
    {
    }
}