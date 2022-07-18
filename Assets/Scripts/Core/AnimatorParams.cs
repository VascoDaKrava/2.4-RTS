using UnityEngine;

namespace Core
{
    public static class AnimatorParams
    {
        public static int Idle = Animator.StringToHash("Idle");
        public static int Walk = Animator.StringToHash("Walk");
        public static int Die = Animator.StringToHash("Die");
        public static int Attack = Animator.StringToHash("Attack");

        public static string DieState = "Dead";
        public static int DieStateInt = Animator.StringToHash("Dead");
    }
}