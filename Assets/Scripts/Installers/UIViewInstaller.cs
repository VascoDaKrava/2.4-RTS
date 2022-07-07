using UserControlSystem.UI.View;
using Zenject;

namespace Installers
{
    public sealed class UIViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ProductionQueueView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}