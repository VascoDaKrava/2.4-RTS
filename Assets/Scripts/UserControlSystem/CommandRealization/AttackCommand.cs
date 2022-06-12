using Abstractions;
using Abstractions.Commands.CommandsInterfaces;


namespace UserControlSystem.CommandsRealization
{
    public sealed class AttackCommand : IAttackCommand
    {
        public AttackCommand(IDamagable target, float damage)
        {
            target.GetDamage(damage);
        }
    }
}