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

        public MoveCommand(Vector3 target)
        {
            Targets = new Vector3[] { target };
        }
    }
}