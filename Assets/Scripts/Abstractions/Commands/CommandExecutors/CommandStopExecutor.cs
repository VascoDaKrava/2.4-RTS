using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using Zenject;


namespace Abstractions.Commands.CommandExecutors
{
    public sealed class CommandStopExecutor : CommandExecutorBase<IStopCommand>
    {
        [Inject(Id = 13)] private GameObject _test;

        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            Debug.Log($"{name} do {command.GetType()}");
            Debug.Log($"Test = {_test}");
        }
    }
}