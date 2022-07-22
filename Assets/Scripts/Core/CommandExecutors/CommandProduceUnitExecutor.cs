using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using Core;
using Core.CommandExecutors;
using UniRx;
using UnityEngine;
using UserControlSystem.CommandsRealization;
using Zenject;

public class CommandProduceUnitExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
{
    [Inject] private DiContainer _container;

    [SerializeField] private FactionMember _faction;
    [SerializeField] private Transform _unitsParent;
    [SerializeField] private int _maximumUnitsInQueue = 5;
    [SerializeField] private Transform _unitCreationPosition;

    private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();

    public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;

    private void Update()
    {
        if (_queue.Count == 0)
        {
            return;
        }

        var innerTask = (UnitProductionTask)_queue[0];
        innerTask.TimeLeft -= Time.deltaTime;

        if (innerTask.TimeLeft <= 0)
        {
            removeTaskAtIndex(0);
            
            var unit = _container.InstantiatePrefab(innerTask.UnitPrefab, _unitCreationPosition.position, Quaternion.identity, _unitsParent);

            unit.GetComponent<CommandMoveExecutor>()
                .ExecuteSpecificCommand(
                new MoveCommand(
                    new Vector3[] { gameObject.GetComponent<IHolderRallyPoint>().RallyPoint }
                    )
                );

            unit.GetComponent<FactionMember>().SetFaction(_faction.FactionID);
        }
    }

    public void Cancel(int index) => removeTaskAtIndex(index);

    private void removeTaskAtIndex(int index)
    {
        for (int i = index; i < _queue.Count - 1; i++)
        {
            _queue[i] = _queue[i + 1];
        }

        _queue.RemoveAt(_queue.Count - 1);
    }

    public override void ExecuteSpecificCommand(ICommand baseCommand)
    {
        var command = (IProduceUnitCommand)baseCommand;

        if (_queue.Count < _maximumUnitsInQueue)
        {
            _queue.Add(new UnitProductionTask(command.ProductionTime, command.Icon, command.UnitPrefab, command.Name));
        }
    }
}
