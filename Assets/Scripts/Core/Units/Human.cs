using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace Core
{
    public sealed class Human : UnitBase, IProduceUnitCommand
    {
        [SerializeField] private float _attackStrength = 25.0f;
        [SerializeField] private float _maxHealth = 100.0f;
        [SerializeField] private float _health = 100.0f;
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _selectionMarker;

        [SerializeField] private float _productionTime = 3.0f;
        [SerializeField] private string _unitName = "Human";

        public override float AttackStrength => _attackStrength;
        public override float MaxHealth => _maxHealth;
        public override float Health => _health;
        public override Sprite Icon => _icon;
        public override GameObject SelectionMarker => _selectionMarker;

        public float ProductionTime => _productionTime;
        public GameObject UnitPrefab => gameObject;
        public string UnitName => _unitName;
    }
}
