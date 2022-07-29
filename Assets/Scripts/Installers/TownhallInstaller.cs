using Abstractions;
using Zenject;

public sealed class TownhallInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IHolderCommandExecutor>().FromComponentInChildren();
    }
}