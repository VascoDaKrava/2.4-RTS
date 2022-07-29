using Abstractions.Commands;

namespace Core
{
    public sealed partial class AutoAttackEvaluator
    {
        public class AttackerParallelnfo
        {
            public float VisionRadius;
            public ICommand CurrentCommand;

            public AttackerParallelnfo(float visionRadius, ICommand currentCommand)
            {
                VisionRadius = visionRadius;
                CurrentCommand = currentCommand;
            }
        }
    }
}