using Abstractions;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class PrefabsInstaller : MonoInstaller
    {
        [SerializeField] private List<UnitBase> _units;

        public override void InstallBindings()
        {
            foreach (var unit in _units)
            {
                Container
                    .Bind<GameObject>()
                    .WithId($"{unit.Name}.GameObject")
                    .FromInstance(unit.gameObject)
                    .AsTransient();

                Container
                    .Bind<float>()
                    .WithId($"{unit.Name}.ProductionTime")
                    .FromInstance(unit.ProductionTime);

                Container
                    .Bind<string>()
                    .WithId($"{unit.Name}.Name")
                    .FromInstance(unit.Name);

                Container
                    .Bind<Sprite>()
                    .WithId($"{unit.Name}.Icon")
                    .FromInstance(unit.Icon);
            }
        }
    }
}