using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using Utils;
using Zenject;


namespace UserControlSystem
{
    public class UIModelInstaller : MonoInstaller
    {
        [SerializeField] private AssetsContext _legacyContext;
        [SerializeField] private Vector3Value _vector3Value;
        [SerializeField] private DamagableValue _damagableValue;
        [SerializeField] private AttackerValue _attackableleValue;
        [SerializeField] private SelectableValue _selectableleValue;

        public override void InstallBindings()
        {
            Container.Bind<AssetsContext>().FromInstance(_legacyContext);
            Container.Bind<Vector3Value>().FromInstance(_vector3Value);
            Container.Bind<DamagableValue>().FromInstance(_damagableValue);
            Container.Bind<AttackerValue>().FromInstance(_attackableleValue);
            Container.Bind<SelectableValue>().FromInstance(_selectableleValue);

            Container.Bind<CommandCreatorBase<IProduceUnitCommand>>()
                .To<ProduceUnitCommandCommandCreator>().AsTransient();

            Container.Bind<CommandCreatorBase<IAttackCommand>>()
                .To<AttackCommandCommandCreator>().AsTransient();

            Container.Bind<CommandCreatorBase<IMoveCommand>>()
                .To<MoveCommandCommandCreator>().AsTransient();

            Container.Bind<CommandCreatorBase<IPatrolCommand>>()
                .To<PatrolCommandCommandCreator>().AsTransient();

            Container.Bind<CommandCreatorBase<IStopCommand>>()
                .To<StopCommandCommandCreator>().AsTransient();

            Container.Bind<CommandButtonsModel>().AsTransient();
        }
    }
}