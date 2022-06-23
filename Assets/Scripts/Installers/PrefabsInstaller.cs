using UnityEngine;
using Zenject;


public sealed class PrefabsInstaller : MonoInstaller
{
    [SerializeField] private GameObject _unitHuman;

    public override void InstallBindings()
    {
        Container
            .Bind<GameObject>()
            .WithId(13)
            .FromInstance(_unitHuman)
            .AsTransient()
            ;
    }
}
