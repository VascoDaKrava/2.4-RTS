using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

namespace UserControlSystem.UI.Presenter
{
    public class FinishGamePresenter : MonoBehaviour
    {
        [Inject] private IGameState _gameState;
        [SerializeField] private IFinishGameView _view;

        [Inject]
        private void Init()
        {
            _gameState.WhoWin
                .ObserveOnMainThread()
                .Subscribe(result =>
                {
                    _view.SetWin(result);
                    _view.ShowMessage = true;
                    Time.timeScale = 0.0f;
                }
            );
        }
    }
}