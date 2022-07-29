using Abstractions;
using Abstractions.Commands.CommandsInterfaces;

namespace UserControlSystem.CommandsRealization
{
    public class AutoAttackCommand : IAttackCommand
    {
        public IDamagable Target { get; private set; }

        public AutoAttackCommand(IDamagable target)
        {
            Target = target;
        }
    }
}