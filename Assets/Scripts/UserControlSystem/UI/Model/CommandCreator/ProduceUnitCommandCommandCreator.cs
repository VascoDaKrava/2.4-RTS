using System;
using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using UserControlSystem.CommandsRealization;
using Zenject;

namespace UserControlSystem
{
    public sealed class ProduceUnitCommandCommandCreator : CommandCreatorBase<IProduceUnitCommand<UnitBase>>
    {
        [Inject] private DiContainer _container;

        protected override void ClassSpecificCommandCreation(Action<IProduceUnitCommand<UnitBase>> creationCallback)
        {
            var unitCommand = new ProduceUnitCommandHeir();
            _container.Inject(unitCommand);
            creationCallback?.Invoke(unitCommand);
        }
    }
}