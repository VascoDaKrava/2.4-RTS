using System.Threading;
using Zenject;

public class HumanInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<CancellationTokenSource>()
            .FromInstance(new CancellationTokenSource())
            .AsSingle();
    }
}