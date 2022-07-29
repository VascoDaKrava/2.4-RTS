using Abstractions;
using Abstractions.Commands.CommandsInterfaces;

namespace UserControlSystem.CommandsRealization
{
    public sealed class AttackCommand : IAttackCommand
    {
        public IDamagable Target { get; private set; }

        public AttackCommand(IDamagable target, float damage)
        {
            Target = target;
        }
    }
}