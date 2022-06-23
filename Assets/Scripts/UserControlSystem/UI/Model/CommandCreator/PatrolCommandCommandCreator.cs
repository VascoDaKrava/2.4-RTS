using System;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using UserControlSystem.CommandsRealization;
using Zenject;


namespace UserControlSystem
{
    public sealed class PatrolCommandCommandCreator : CommandCreatorBase<IPatrolCommand>
    {
        [Inject] private SelectableValue _selectableValue;

        private Action<IPatrolCommand> _creationCallback;

        [Inject]
        private void Init(Vector3Value groundClicks) => groundClicks.OnValueChange += ONNewValue;

        private void ONNewValue(Vector3 groundClick)
        {
            _creationCallback?.Invoke(new PatrolCommand(_selectableValue.CurrentValue.Position, groundClick));
            _creationCallback = null;
        }

        protected override void ClassSpecificCommandCreation(Action<IPatrolCommand> creationCallback)
            => _creationCallback = creationCallback;

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            _creationCallback = null;
        }
    }
}