using Abstractions;
using Core;
using Core.CommandExecutors;
using UnityEngine;
using Zenject;

public sealed class SkeletonInstaller : MonoInstaller
{
    [SerializeField] private Skeleton _skeleton;
    [SerializeField] private AttackerParallelnfoUpdater _attacker;
    [SerializeField] private FactionMemberParallelInfoUpdater _factionMember;

    public override void InstallBindings()
    {
        Container.Bind<UnitCTSource>().FromComponentInChildren();
        Container.Bind<UnitMovementStop>().FromComponentInChildren();
        Container.Bind<CommandStopExecutor>().FromComponentInChildren();
        Container.Bind<CommandMoveExecutor>().FromComponentInChildren();

        Container.Bind<IAttacker>().FromComponentInChildren();
        Container.Bind<IHolderAnimator>().FromComponentInChildren();
        Container.Bind<IHolderHealth>().FromComponentInChildren();
        Container.Bind<IHolderNavMeshAgent>().FromComponentInChildren();
        Container.Bind<IHolderUnitMovementStop>().FromComponentInChildren();
        Container.Bind<IHolderCommandExecutor>().FromComponentInChildren();

        Container.Bind<ISelectable>().FromComponentInChildren();

        Container.Bind<IAutomaticAttacker>().FromComponentInChildren();
        Container.Bind<IFactionMember>().FromComponentInChildren();
        Container.Bind<ITickable>().FromInstance(_attacker);
        Container.Bind<ITickable>().FromInstance(_factionMember);

        Container.Bind<float>().WithId("AttackRange").FromInstance(_skeleton.AttackRange);
        Container.Bind<int>().WithId("AttackPeriod").FromInstance(_skeleton.AttackPeriod);
        Container.Bind<string>().WithId("Name").FromInstance(_skeleton.Name);
    }
}