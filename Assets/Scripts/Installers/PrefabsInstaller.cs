using Core;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class PrefabsInstaller : MonoInstaller
    {
        [SerializeField] private Human _unitHuman;

        public override void InstallBindings()
        {
            Container
                .Bind<GameObject>()
                .WithId("Human.GameObject")
                .FromInstance(_unitHuman.gameObject)
                .AsTransient();

            Container
                .Bind<float>()
                .WithId("Human.ProductionTime")
                .FromInstance(_unitHuman.ProductionTime);

            Container
                .Bind<string>()
                .WithId("Human.Name")
                .FromInstance(_unitHuman.Name);

            Container
                .Bind<Sprite>()
                .WithId("Human.Icon")
                .FromInstance(_unitHuman.Icon);
        }
    }
}