using System;
using Abstractions.Commands.CommandsInterfaces;
using UserControlSystem.CommandsRealization;


namespace UserControlSystem
{
    public sealed class ProduceUnitCommandCommandCreator : CommandCreatorBase<IProduceUnitCommand>
    {
        protected override void ClassSpecificCommandCreation(Action<IProduceUnitCommand> creationCallback)
            => creationCallback?.Invoke(new ProduceUnitCommandHeir());
    }
}