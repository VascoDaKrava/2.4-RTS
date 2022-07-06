using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;


namespace UserControlSystem.CommandsRealization
{
    public sealed class PatrolCommand : IPatrolCommand
    {
        public Vector3 StartPoint { get; }
        public Vector3 FinishPoint { get; }

        public PatrolCommand(Vector3 startPoint, Vector3 finishPoint)
        {
            StartPoint = startPoint;
            FinishPoint = finishPoint;
        }
    }
}