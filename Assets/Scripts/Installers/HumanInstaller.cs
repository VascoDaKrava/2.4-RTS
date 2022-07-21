using Abstractions;
using Core;
using System.Threading;
using UnityEngine;
using Zenject;

public sealed class HumanInstaller : MonoInstaller
{
    [SerializeField] private Human _human;

    public override void InstallBindings()
    {
        Container
            .Bind<CancellationTokenSource>()
            .FromInstance(new CancellationTokenSource())
            .AsSingle();

        Container.Bind<IHolderHealth>().FromComponentInChildren();
        
        Container.Bind<IHolderNavMeshAgent>().FromComponentInChildren();
        
        Container.Bind<IAttacker>().FromComponentInChildren();

        Container.Bind<float>().WithId("AttackRange").FromInstance(_human.AttackRange);
        
        Container.Bind<int>().WithId("AttackPeriod").FromInstance(_human.AttackPeriod);
    }
}