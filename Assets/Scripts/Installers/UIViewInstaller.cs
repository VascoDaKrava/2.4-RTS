using Abstractions;
using UnityEngine;
using UserControlSystem.UI.Presenter;
using UserControlSystem.UI.View;
using Zenject;

namespace Installers
{
    public sealed class UIViewInstaller : MonoInstaller
    {
        [SerializeField] private FinishGameView _finishGameView;
        [SerializeField] private BottomRightPresenter _bottomRightPresenter;

        public override void InstallBindings()
        {
            Container.Bind<ProductionQueueView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IFinishGameView>().FromInstance(_finishGameView);
            Container.Bind<ICommandButtonsPresenter>().FromInstance(_bottomRightPresenter);
        }
    }
}