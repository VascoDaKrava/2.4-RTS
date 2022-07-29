using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using Core;
using Core.CommandExecutors;
using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UserControlSystem.CommandsRealization;
using Zenject;

public class CommandProduceUnitExecutor : CommandExecutorBase<IProduceUnitCommand<UnitBase>>, IUnitProducer
{
    [Inject] private DiContainer _container;

    private IHolderRallyPoint _holderRallyPoint;
    private IDisposable _creationObserver;
    private bool _isInWork = false;

    private bool IsInWork
    {
        get => _isInWork;
        set
        {
            _isInWork = value;
            if (!_isInWork)
            {
                _creationObserver?.Dispose();
            }
        }
    }

    [Space]
    [SerializeField] private FactionMember _faction;
    [SerializeField] private Transform _unitsParent;
    [SerializeField] private int _maximumUnitsInQueue = 5;
    [SerializeField] private Transform _unitCreationPosition;

    private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();

    public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;

    private void Start()
    {
        _holderRallyPoint = gameObject.GetComponent<IHolderRallyPoint>();

        _queue
            .ObserveCountChanged()
            .Where(_ => _queue.Count > 0)
            .Where(_ => !IsInWork)
            .Subscribe(_ => StartTimer())
            .AddTo(this);
    }

    private void StartTimer()
    {
        var innerTask = (UnitProductionTask)_queue[0];
        IsInWork = true;

        _creationObserver = Observable
            .EveryUpdate()
            .Where(_ => innerTask.TimeLeft > 0.0f)
            .Subscribe(_ =>
            {
                innerTask.TimeLeft -= Time.deltaTime;
                if (innerTask.TimeLeft <= 0)
                {
                    CreateUnit(innerTask);
                    removeTaskAtIndex(0);
                }
            })
            .AddTo(this);
    }

    private void CreateUnit(UnitProductionTask innerTask)
    {
        var unit = _container.InstantiatePrefab(innerTask.UnitPrefab, _unitCreationPosition.position, Quaternion.identity, _unitsParent);
        unit.GetComponent<FactionMember>().SetFaction(_faction.FactionID);

        unit.GetComponent<CommandMoveExecutor>()
            .TryExecuteCommand(
            new MoveCommand(
                new Vector3[] { _holderRallyPoint.RallyPoint }
                )
            );
    }

    private void removeTaskAtIndex(int index)
    {
        for (int i = index; i < _queue.Count - 1; i++)
        {
            _queue[i] = _queue[i + 1];
        }

        if (index == 0)
        {
            IsInWork = false;
        }

        _queue.RemoveAt(_queue.Count - 1);
    }

    public void Cancel(int index) => removeTaskAtIndex(index);

    public override async Task ExecuteSpecificCommand(ICommand baseCommand)
    {
        await Task.Yield();
        var command = (IProduceUnitCommand<UnitBase>)baseCommand;

        if (_queue.Count < _maximumUnitsInQueue)
        {
            _queue.Add(new UnitProductionTask(command.ProductionTime, command.Icon, command.UnitPrefab, command.Name));
        }
    }
}
