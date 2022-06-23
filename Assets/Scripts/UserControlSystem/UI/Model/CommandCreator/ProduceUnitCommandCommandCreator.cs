using System;
using Abstractions.Commands.CommandsInterfaces;
using UserControlSystem.CommandsRealization;
using Zenject;


namespace UserControlSystem
{
    public sealed class ProduceUnitCommandCommandCreator : CommandCreatorBase<IProduceUnitCommand>
    {

        [Inject] private DiContainer _container;

        protected override void ClassSpecificCommandCreation(Action<IProduceUnitCommand> creationCallback)
        {
            var unitCommand = new ProduceUnitCommandHeir();
            _container.Inject(unitCommand);
            creationCallback?.Invoke(unitCommand);
        }

        //protected override void ClassSpecificCommandCreation(Action<IProduceUnitCommand> creationCallback)
        //    => creationCallback?.Invoke(new ProduceUnitCommandHeir());
    }
}