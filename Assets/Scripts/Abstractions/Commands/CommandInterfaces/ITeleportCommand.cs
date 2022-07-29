using UnityEngine;


namespace Abstractions.Commands.CommandsInterfaces
{
    public interface ITeleportCommand : ICommand
    {
        public Vector3 Target { get; }
    }
}