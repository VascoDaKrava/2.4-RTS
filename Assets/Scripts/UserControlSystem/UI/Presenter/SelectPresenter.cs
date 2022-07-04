using Abstractions;
using UnityEngine;
using UserControlSystem;


public sealed class SelectPresenter : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private SelectableValue _selectable;

    private ISelectable _lastSelected;

    #endregion


    #region UnityMethods

    void Start()
    {
        _selectable.OnValueChange += SelectHandler;
    }

    private void OnDestroy()
    {
        _selectable.OnValueChange -= SelectHandler;
    }
    
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
