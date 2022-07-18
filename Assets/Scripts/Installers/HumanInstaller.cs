using Abstractions;
using System.Threading;
using UnityEngine;
using Zenject;

public class HumanInstaller : MonoInstaller
{
    [SerializeField] private float _attackDistance = 222.0f;

    /// <summary>
    /// In ms
    /// </summary>
    [SerializeField] private int _attackPeriod = 22222;

    public override void InstallBindings()
    {
        Container
            .Bind<CancellationTokenSource>()
            .FromInstance(new CancellationTokenSource())
            .AsSingle();

        Container.Bind<IHolderHealth>().FromComponentInChildren();
        
        Container.Bind<float>().WithId("AttackDistance").FromInstance(2.0f);
        
        Container.Bind<int>().WithId("AttackPeriod").FromInstance(_attackPeriod);
    }
}