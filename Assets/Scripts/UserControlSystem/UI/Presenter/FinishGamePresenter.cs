using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

namespace UserControlSystem.UI.Presenter
{
    public class FinishGamePresenter : MonoBehaviour
    {
        [Inject] private IGameState _gameState;
        [Inject] private IFinishGameView _view;
        [SerializeField] private GameObject _otherUI;

        [Inject]
        private void Init()
        {
            _gameState.WhoWin
                .ObserveOnMainThread()
                .Subscribe(result =>
                {
                    _otherUI.SetActive(false);
                    _view.SetWin(result);
                    _view.ShowMessage = true;
                    Time.timeScale = 0.0f;
                });
        }
    }
}