using System;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using UserControlSystem.CommandsRealization;
using Zenject;


namespace UserControlSystem
{
    public sealed class MoveCommandCommandCreator : CommandCreatorBase<IMoveCommand>
    {
        private Action<IMoveCommand> _creationCallback;

        [Inject]
        private void Init(Vector3Value groundClicks) => groundClicks.OnValueChange += ONNewValue;
        
        private void ONNewValue(Vector3 groundClick)
        {
            _creationCallback?.Invoke(new MoveCommand(groundClick));
            _creationCallback = null;
        }

        protected override void ClassSpecificCommandCreation(Action<IMoveCommand> creationCallback) 
            => _creationCallback = creationCallback;

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            _creationCallback = null;
        }
    }
}