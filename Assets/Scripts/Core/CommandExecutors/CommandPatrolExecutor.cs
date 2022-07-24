using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading.Tasks;
using UnityEngine;
using UserControlSystem.CommandsRealization;
using Zenject;

namespace Core.CommandExecutors
{
    public sealed class CommandPatrolExecutor : CommandExecutorBase<IPatrolCommand>
    {
        [Inject] private UnitMovementStop _unitMoveStop;
        [Inject] private UnitCTSource _unitCTSource;
        [Inject] private CommandMoveExecutor _moveExecutor;

        private Vector3[] _patrolPoints;

        public override async Task ExecuteSpecificCommand(ICommand baseCommand)
        {
            var command = (IPatrolCommand)baseCommand;

            _patrolPoints = new Vector3[] { command.FinishPoint, command.StartPoint };

            while (true)
            {
                await _moveExecutor.ExecuteSpecificCommand(new MoveCommand(_patrolPoints));
                
                try
                {
                    await _unitMoveStop.WithCancellation(_unitCTSource.Token);
                }
                catch
                {
                    _unitMoveStop.DoStop();
                    break;
                }
            }
        }
    }
}