using UnityEngine;


namespace Abstractions
{
    public interface ISelectable : IIconHolder, IHealthHolder
    {
        bool Selected { get; set; }
        Vector3 Position { get; set; }
    }
}