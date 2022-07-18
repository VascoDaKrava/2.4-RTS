using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;


namespace UserControlSystem.CommandsRealization
{
    public sealed class MoveCommand : IMoveCommand
    {
        public Vector3[] Targets { get; }

        public MoveCommand(Vector3[] targets)
        {
            Targets = targets;
        }
    }
}