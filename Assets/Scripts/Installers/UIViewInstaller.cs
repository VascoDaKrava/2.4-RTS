using Abstractions;
using UnityEngine;
using UserControlSystem.UI.View;
using Zenject;

namespace Installers
{
    public sealed class UIViewInstaller : MonoInstaller
    {
        [SerializeField] FinishGameView _finishGameView;

        public override void InstallBindings()
        {
            Container.Bind<ProductionQueueView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IFinishGameView>().FromInstance(_finishGameView);
        }
    }
}