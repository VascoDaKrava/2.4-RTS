using Abstractions;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class PrefabsInstaller : MonoInstaller
    {
        [SerializeField] private UnitBase _human;
        [SerializeField] private UnitBase _skeleton;

        public override void InstallBindings()
        {
            Container
                .Bind<GameObject>()
                .WithId("Human.GameObject")
                .FromInstance(_human.gameObject)
                .AsTransient();

            Container
                .Bind<float>()
                .WithId("Human.ProductionTime")
                .FromInstance(_human.ProductionTime);

            Container
                .Bind<string>()
                .WithId("Human.Name")
                .FromInstance(_human.Name);

            Container
                .Bind<Sprite>()
                .WithId("Human.Icon")
                .FromInstance(_human.Icon);

            Container
                .Bind<GameObject>()
                .WithId("Skeleton.GameObject")
                .FromInstance(_skeleton.gameObject)
                .AsTransient();

            Container
                .Bind<float>()
                .WithId("Skeleton.ProductionTime")
                .FromInstance(_skeleton.ProductionTime);

            Container
                .Bind<string>()
                .WithId("Skeleton.Name")
                .FromInstance(_skeleton.Name);

            Container
                .Bind<Sprite>()
                .WithId($"{_skeleton.Name}.{_skeleton.Icon}")
                .FromInstance(_skeleton.Icon);
        }
    }
}