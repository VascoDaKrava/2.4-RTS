using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using UserControlSystem;
using UserControlSystem.UI.Model;
using Zenject;

namespace Installers
{
    public class UIModelInstaller : MonoInstaller
    {
        [SerializeField] private Sprite _humanUnitIcon;

        public override void InstallBindings()
        {
            Container
                .Bind<CommandCreatorBase<IProduceUnitCommand>>()
                .To<ProduceUnitCommandCommandCreator>()
                .AsTransient();

            Container
                .Bind<CommandCreatorBase<IAttackCommand>>()
                .To<AttackCommandCommandCreator>()
                .AsTransient();

            Container
                .Bind<CommandCreatorBase<IMoveCommand>>()
                .To<MoveCommandCommandCreator>()
                .AsTransient();

            Container
                .Bind<CommandCreatorBase<IPatrolCommand>>()
                .To<PatrolCommandCommandCreator>()
                .AsTransient();

            Container
                .Bind<CommandCreatorBase<IStopCommand>>()
                .To<StopCommandCommandCreator>()
                .AsTransient();

            Container
                .Bind<CommandCreatorBase<ISetRallyPointCommand>>()
                .To<SetRallyPointCommandCreator>()
                .AsTransient();

            Container
                .Bind<CommandButtonsModel>()
                .AsTransient();

            Container
                .Bind<UnitProducerModel>()
                .AsSingle();
        }
    }
}