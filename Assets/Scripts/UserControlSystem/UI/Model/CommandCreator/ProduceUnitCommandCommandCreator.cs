using System;
using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using Core;
using UserControlSystem.CommandsRealization;
using Zenject;

namespace UserControlSystem
{
    public sealed class ProduceUnitCommandCommandCreator : CommandCreatorBase<IProduceUnitCommand<UnitBase>>
    {
        [Inject] private DiContainer _container;

        protected override void ClassSpecificCommandCreation(Action<IProduceUnitCommand<UnitBase>> creationCallback, Type type)
        {
            if (type == typeof(Human))
            {
                var humanCommand = new ProduceUnitHumanCommand();
                _container.Inject(humanCommand);
                creationCallback?.Invoke(humanCommand);
                return;
            }

            if (type == typeof(Skeleton))
            {
                var skeletonCommand = new ProduceUnitSkeletonCommand();
                _container.Inject(skeletonCommand);
                creationCallback?.Invoke(skeletonCommand);
                return;
            }
        }
    }
}