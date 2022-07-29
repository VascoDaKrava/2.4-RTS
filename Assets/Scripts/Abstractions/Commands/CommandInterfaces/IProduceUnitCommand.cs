using UnityEngine;

namespace Abstractions.Commands.CommandsInterfaces
{
    public interface IProduceUnitCommand<T> : ICommand, IHolderIcon, IHolderName
        where T : UnitBase
    {
        float ProductionTime { get; }
        GameObject UnitPrefab { get; }
    }
}