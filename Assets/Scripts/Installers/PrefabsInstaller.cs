using Core;
using UnityEngine;
using Zenject;


public sealed class PrefabsInstaller : MonoInstaller
{
    [SerializeField] private GameObject _unitHuman;

    public override void InstallBindings()
    {
        Container
            .Bind<GameObject>()
            .WithId("UnitHuman")
            .FromInstance(_unitHuman)
            .AsTransient();

        Container
            .Bind<float>()
            .WithId("UnitHuman")
            .FromInstance(_unitHuman.GetComponent<Human>().ProductionTime);

        Container
            .Bind<string>()
            .WithId("UnitHuman")
            .FromInstance(_unitHuman.GetComponent<Human>().Name);

        Container
            .Bind<Sprite>()
            .WithId("UnitHuman")
            .FromInstance(_unitHuman.GetComponent<Human>().Icon);
    }
}
