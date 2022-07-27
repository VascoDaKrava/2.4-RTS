using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core
{
    public class AutoMover : MonoBehaviour
    {
        [Inject] ICommandButtonsPresenter _commandButtons;

        private void Start()
        {
            _commandButtons.IsCommandPending
                .Subscribe(state => Debug.Log("Com " + state))
                .AddTo(this);
        }
    }
}