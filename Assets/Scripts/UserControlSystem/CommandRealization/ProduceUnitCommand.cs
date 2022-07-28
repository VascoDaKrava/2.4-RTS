using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using Zenject;


namespace UserControlSystem.CommandsRealization
{
    public class ProduceUnitCommand : IProduceUnitCommand<UnitBase>
    {
        [Inject(Id = "Human.GameObject")] private GameObject _unitPrefab;
        [Inject(Id = "Human.Name")] public string Name { get; }
        [Inject(Id = "Human.Icon")] public Sprite Icon { get; }
        [Inject(Id = "Human.ProductionTime")] public float ProductionTime { get; }

        public GameObject UnitPrefab => _unitPrefab;
    }
}