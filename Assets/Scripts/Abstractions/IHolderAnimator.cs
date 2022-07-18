using UnityEngine;

namespace Abstractions
{
    public interface IHolderAnimator
    {
        public abstract Animator Animator { get; }
    }
}