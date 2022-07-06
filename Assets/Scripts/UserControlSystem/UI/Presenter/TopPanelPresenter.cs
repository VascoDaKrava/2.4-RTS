using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;
using System;
using Abstractions;

public sealed class TopPanelPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textTime;
    [SerializeField] private Button _menuButton;
    [SerializeField] private GameObject _menu;

    [Inject]
    private void Init(ITimeModel timeModel)
    {
        timeModel.GameTime.Subscribe(seconds =>
        {
            var t = TimeSpan.FromSeconds(seconds);
            _textTime.text = $"{t.Minutes:D2}:{t.Seconds:D2}";
        });

        _menuButton.OnClickAsObservable().Subscribe(_ => _menu.SetActive(true));
    }
}