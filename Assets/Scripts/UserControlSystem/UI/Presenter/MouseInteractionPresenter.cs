using System.Linq;
using Abstractions;
using UnityEngine;
using UnityEngine.EventSystems;
using UserControlSystem;


public sealed class MouseInteractionPresenter : MonoBehaviour
{
    private const string GROUND_TAG = "Ground";
    
    [SerializeField] private Camera _camera;
    [SerializeField] private SelectableValue _selectedObject;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private Vector3Value _groundPointClick;

    private void Update()
    {
        if (!Input.GetMouseButtonUp(0) &&
            !Input.GetMouseButtonUp(1)
            )
        {
            return;
        }

        if (_eventSystem.IsPointerOverGameObject())
        {
            return;
        }

        var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));

        if (hits.Length == 0)
        {
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            LeftButtonClickHandler(hits);
        }

        if (Input.GetMouseButtonUp(1))
        {
            RightButtonClickHandler(hits);
        }

    }

    private void LeftButtonClickHandler(RaycastHit[] hits)
    {
        _selectedObject.SetValue(null);

        var selectable = hits
            .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
            .FirstOrDefault(c => c != null);
        _selectedObject.SetValue(selectable);
    }

    private void RightButtonClickHandler(RaycastHit[] hits)
    {
        var groundPoint = hits.FirstOrDefault(hit => hit.transform.gameObject.CompareTag(GROUND_TAG)).point;
        _groundPointClick.SetValue(groundPoint);
    }
}