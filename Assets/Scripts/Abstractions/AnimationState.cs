using UnityEngine;

namespace Abstractions
{
    public static class AnimationState
    {
        public static int Idle = Animator.StringToHash("Idle");
        public static int Walk = Animator.StringToHash("Walk");
    }
}