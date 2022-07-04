using UnityEngine;

namespace Abstractions.Commands.CommandsInterfaces
{
    public interface IPatrolCommand : ICommand
    {
        Vector3 StartPoint { get; }
        Vector3 FinishPoint { get; }
    }
}