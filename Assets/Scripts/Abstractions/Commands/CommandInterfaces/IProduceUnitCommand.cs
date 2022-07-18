using UnityEngine;

namespace Abstractions.Commands.CommandsInterfaces
{
    public interface IProduceUnitCommand : ICommand, IHolderIcon, IHolderName
    {
        float ProductionTime { get; }
        GameObject UnitPrefab { get; }
    }
}