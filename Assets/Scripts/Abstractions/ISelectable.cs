using UnityEngine;


namespace Abstractions
{
    public interface ISelectable
    {
        float Health { get; }
        float MaxHealth { get; }
        Sprite Icon { get; }
        bool Selected { get; set; }
        Vector3 Position { get; set; }
    }
}