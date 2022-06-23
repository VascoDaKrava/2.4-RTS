using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using Zenject;


namespace UserControlSystem.CommandsRealization
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        //[InjectAsset("Human")] private GameObject _unitPrefab;
        [Inject(Id = 13)] private GameObject _unitPrefab;

        public GameObject UnitPrefab
        {
            get
            {
                Debug.Log("_unitPrefab = " + _unitPrefab);
                return _unitPrefab;
            }
        }
    }
}