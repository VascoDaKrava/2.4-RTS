using System;
using Abstractions.Commands.CommandsInterfaces;
using Utils;
using Zenject;


namespace UserControlSystem
{
    public sealed class PatrolCommandCommandCreator : CommandCreatorBase<IPatrolCommand>
    {
        [Inject] private AssetsContext _context;

        protected override void ClassSpecificCommandCreation(Action<IPatrolCommand> creationCallback)
        {
            
        }
    }
}