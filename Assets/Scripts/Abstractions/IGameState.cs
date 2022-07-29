using Abstractions.Enums;
using System;

namespace Abstractions
{
    public interface IGameState
    {
        void AddFactionMember(IFactionMember factionMember);
        void RemoveFactionMember(IFactionMember factionMember);
        IObservable<FactionID> WhoWin { get; }
    }
}
