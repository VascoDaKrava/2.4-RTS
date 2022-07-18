using System;
using Abstractions;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserControlSystem.UI.View
{
    public class ProductionQueueView : MonoBehaviour
    {
        [SerializeField] private Slider _productionProgressSlider;
        [SerializeField] private TextMeshProUGUI _currentUnitName;
        [SerializeField] private Image _currentUnitImage;

        [SerializeField] private Image[] _images;
        [SerializeField] private GameObject[] _imageHolders;
        [SerializeField] private Button[] _buttons;

        private Subject<int> _cancelButtonClicks = new Subject<int>();

        private IDisposable _unitProductionTaskCt;

        public IObservable<int> CancelButtonClicks => _cancelButtonClicks;

        [Inject]
        private void Init()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                var index = i;
                _buttons[i].onClick.AddListener(() => _cancelButtonClicks.OnNext(index));
            }
        }

        private void SetCellActive(IUnitProductionTask task, bool state)
        {
            _productionProgressSlider.gameObject.SetActive(state);
            _currentUnitImage.enabled = state;
            _currentUnitName.enabled = state;
            
            if (state)
            {
                _currentUnitImage.sprite = task.Icon;
                _currentUnitName.text = task.Name;
            }
            else
            {
                _currentUnitName.text = string.Empty;
                _unitProductionTaskCt?.Dispose();
            }
        }

        public void Clear()
        {
            for (int i = 0; i < _images.Length; i++)
            {
                _images[i].sprite = null;
                _imageHolders[i].SetActive(false);
            }
            SetCellActive(default, false);
        }

        public void SetTask(IUnitProductionTask task, int index)
        {
            if (task == null)
            {
                _imageHolders[index].SetActive(false);
                _images[index].sprite = null;

                if (index == 0)
                {
                    SetCellActive(default, false);
                }
            }
            else
            {

                _imageHolders[index].SetActive(true);
                _images[index].sprite = task.Icon;

                if (index == 0)
                {
                    SetCellActive(task, true);
                    _unitProductionTaskCt = Observable
                        .EveryUpdate()
                        .Subscribe(_ =>
                        {
                            _productionProgressSlider.value = task.TimeLeft / task.ProductionTime;
                        });
                }
            }
        }
    }
}