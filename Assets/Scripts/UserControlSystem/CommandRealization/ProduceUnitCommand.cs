using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using Zenject;


namespace UserControlSystem.CommandsRealization
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        [Inject(Id = "UnitHuman")] private GameObject _unitPrefab;
        [Inject(Id = "UnitHuman")] public string UnitName { get; }
        [Inject(Id = "UnitHuman")] public Sprite Icon { get; }
        [Inject(Id = "UnitHuman")] public float ProductionTime { get; }
        
        public GameObject UnitPrefab => _unitPrefab;
    }
}