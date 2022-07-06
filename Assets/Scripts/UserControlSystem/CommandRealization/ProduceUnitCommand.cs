using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using Zenject;


namespace UserControlSystem.CommandsRealization
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        [Inject(Id = 13)] private GameObject _unitPrefab;

        public GameObject UnitPrefab => _unitPrefab;

    }
}