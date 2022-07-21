using Abstractions;
using Core;
using Core.CommandExecutors;
using UnityEngine;
using Zenject;

public sealed class HumanInstaller : MonoInstaller
{
    [SerializeField] private Human _human;

    public override void InstallBindings()
    {
        Container.Bind<UnitCTSource>().FromComponentInChildren();
        Container.Bind<UnitMovementStop>().FromComponentInChildren();
        Container.Bind<CommandStopExecutor>().FromComponentInChildren();

        Container.Bind<IAttacker>().FromComponentInChildren();
        Container.Bind<IHolderAnimator>().FromComponentInChildren();
        Container.Bind<IHolderHealth>().FromComponentInChildren();
        Container.Bind<IHolderNavMeshAgent>().FromComponentInChildren();
        Container.Bind<IHolderUnitMovementStop>().FromComponentInChildren();

        Container.Bind<float>().WithId("AttackRange").FromInstance(_human.AttackRange);
        Container.Bind<int>().WithId("AttackPeriod").FromInstance(_human.AttackPeriod);
        Container.Bind<string>().WithId("HumanName").FromInstance(_human.Name);
    }
}