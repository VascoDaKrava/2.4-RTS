using System.Threading;
using Zenject;

public class HumanInstaller : MonoInstaller
{
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    public override void InstallBindings()
    {
        Container
            .Bind<CancellationTokenSource>()
            .FromInstance(new CancellationTokenSource())
            .AsSingle();
    }
}