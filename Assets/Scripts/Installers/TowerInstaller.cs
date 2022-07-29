using Abstractions;
using Core;
using UnityEngine;
using Zenject;

public sealed class TowerInstaller : MonoInstaller
{
    [SerializeField] private Tower _tower;

    public override void InstallBindings()
    {
        Container.Bind<UnitCTSource>().FromComponentInChildren();
        
        Container.Bind<IHolderCommandExecutor>().FromComponentInChildren();
        Container.Bind<IHolderAnimator>().FromComponentInChildren();
        Container.Bind<IHolderHealth>().FromComponentInChildren();
        
        Container.Bind<IAttacker>().FromComponentInChildren();
    }
}