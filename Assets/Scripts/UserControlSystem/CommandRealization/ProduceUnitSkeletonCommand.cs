using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using Zenject;

namespace UserControlSystem.CommandsRealization
{
    public class ProduceUnitSkeletonCommand : IProduceUnitCommand<UnitBase>
    {
        [Inject(Id = "Skeleton.GameObject")] private GameObject _unitPrefab;
        [Inject(Id = "Skeleton.Name")] public string Name { get; }
        [Inject(Id = "Skeleton.Icon")] public Sprite Icon { get; }
        [Inject(Id = "Skeleton.ProductionTime")] public float ProductionTime { get; }
        
        public GameObject UnitPrefab => _unitPrefab;
    }
}