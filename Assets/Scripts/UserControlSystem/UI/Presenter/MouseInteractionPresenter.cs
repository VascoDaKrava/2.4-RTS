using System.Linq;
using Abstractions;
using UniRx;
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
    [Inject] private GroundPointValue _groundPointClick;
    [Inject] private DamagableValue _damagableValue;
    [Inject] private AttackerValue _attackableValue;

    private void Start()
    {
        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonUp(0))
            .Subscribe(_ => LeftButtonClickHandler());

        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonUp(1))
            .Subscribe(_ => RightButtonClickHandler());
    }

    private void LeftButtonClickHandler()
    {
        if (!CanContinue(out var hits))
        {
            return;
        }

        _selectedObject.SetValue(null);
        _selectedObject.SetValue(HitResult<ISelectable>(hits));

        _attackableValue.SetValue(null);
        _attackableValue.SetValue(HitResult<IAttacker>(hits));
    }

    private void RightButtonClickHandler()
    {
        if (!CanContinue(out var hits))
        {
            return;
        }

        _groundPointClick.SetValue(hits.FirstOrDefault(hit => hit.transform.gameObject.CompareTag(GROUND_TAG)).point);

        if (HitResult<IDamagable>(hits) != default)
        {
            _damagableValue.SetValue(HitResult<IDamagable>(hits));
            _damagableValue.SetValue(null);
        }
    }

    private bool CanContinue(out RaycastHit[] hits)
    {
        hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));

        if (hits.Length == 0)
        {
            return false;
        }

        if (_eventSystem.IsPointerOverGameObject())
        {
            return false;
        }

        return true;
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