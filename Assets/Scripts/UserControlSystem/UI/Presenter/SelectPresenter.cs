using Abstractions;
using UniRx;
using UnityEngine;
using UserControlSystem;
using Zenject;


public sealed class SelectPresenter : MonoBehaviour
{

    #region Fields

    [Inject] private SelectableValue _selectable;

    private ISelectable _lastSelected;

    #endregion


    #region UnityMethods

    private void Start() => _selectable.Subscribe(value => SelectHandler(value));

    #endregion


    #region Methods

    private void SelectHandler(ISelectable value)
    {
        if (value != null)
        {
            _lastSelected = value;
            value.Selected = true;
        }
        else
        {
            if (_lastSelected != default)
            {
                _lastSelected.Selected = false;
                _lastSelected = default;
            }
        }
    }

    #endregion

}
