using System.Linq;
using Abstractions;
using UnityEngine;
using UnityEngine.EventSystems;
using UserControlSystem;
using Zenject;


public sealed class MouseInteractionPresenter : MonoBehaviour
{
    private const string GROUND_TAG = "Ground";

    [SerializeField] private Camera _camera;
    [SerializeField] private EventSystem _eventSystem;

    [Inject] private SelectableValue _selectedObject;
    [Inject] private Vector3Value _groundPointClick;
    [Inject] private DamagableValue _damagableValue;
    [Inject] private AttackerValue _attackableValue;

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
        _selectedObject.SetValue(HitResult<ISelectable>(hits));

        _attackableValue.SetValue(null);
        _attackableValue.SetValue(HitResult<IAttackable>(hits));
    }

    private void RightButtonClickHandler(RaycastHit[] hits)
    {
        _groundPointClick.SetValue(hits.FirstOrDefault(hit => hit.transform.gameObject.CompareTag(GROUND_TAG)).point);

        if (HitResult<IDamagable>(hits) != default)
        {
            _damagableValue.SetValue(HitResult<IDamagable>(hits));
        }
    }

    private T HitResult<T>(RaycastHit[] hits)
    {
        T hitResult = default;

        hitResult = hits
            .Select(hit => hit.collider.GetComponentInParent<T>())
            .FirstOrDefault(c => c != null);

        return hitResult;
    }
}