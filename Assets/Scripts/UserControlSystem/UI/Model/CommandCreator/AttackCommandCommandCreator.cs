using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using UserControlSystem.CommandsRealization;
using Zenject;


namespace UserControlSystem
{
    public sealed class AttackCommandCommandCreator : CancellableCommandCreatorBase<IAttackCommand, IDamagable>
    {
        [Inject] private AttackerValue _attaker;

        protected override IAttackCommand CreateCommand(IDamagable[] target)
        {
            return new AttackCommand(target[0], _attaker.CurrentValue.AttackStrength);
        }
    }
}