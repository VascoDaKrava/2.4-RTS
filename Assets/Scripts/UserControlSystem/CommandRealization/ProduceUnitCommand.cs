﻿using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using Utils;

namespace UserControlSystem.CommandsRealization
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        public GameObject UnitPrefab => _unitPrefab;
        [InjectAsset("Human")] private GameObject _unitPrefab;
    }
}