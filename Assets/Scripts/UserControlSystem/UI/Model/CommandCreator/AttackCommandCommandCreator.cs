using System;
using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using UserControlSystem.CommandsRealization;
using Utils;
using Zenject;


namespace UserControlSystem
{
    public sealed class AttackCommandCommandCreator : CommandCreatorBase<IAttackCommand>
    {
        [Inject] private AssetsContext _context;
        [Inject] private AttackerValue _attackable;

        private Action<IAttackCommand> _creationCallback;

        [Inject]
        private void Init(DamagableValue damagable)
        {
            damagable.OnValueChange += ONNewValue;
        }

        private void ONNewValue(IDamagable target)
        {
            if (target == null)
                return;
            
            _creationCallback?.Invoke(_context.Inject(new AttackCommand(target, _attackable.CurrentValue.AttackStrength)));
            _creationCallback = null;
        }

        protected override void ClassSpecificCommandCreation(Action<IAttackCommand> creationCallback)
        {
            _creationCallback = creationCallback;
        }

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            _creationCallback = null;
        }

    }
}