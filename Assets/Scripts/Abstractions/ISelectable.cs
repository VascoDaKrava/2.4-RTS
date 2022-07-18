using UnityEngine;


namespace Abstractions
{
    public interface ISelectable : IHolderIcon, IHolderHealth
    {
        bool Selected { get; set; }
        Vector3 Position { get; set; }
    }
}