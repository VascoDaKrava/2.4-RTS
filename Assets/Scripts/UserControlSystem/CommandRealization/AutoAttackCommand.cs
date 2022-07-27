using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace UserControlSystem.CommandsRealization
{
    public class AutoAttackCommand : IAttackCommand
    {
        public IDamagable Target { get; private set; }

        public AutoAttackCommand(IDamagable target)
        {
            Debug.Log($"{this} --> {target}");
            Target = target;
        }
    }
}