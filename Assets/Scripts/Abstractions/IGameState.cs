using Abstractions.Enums;
using System;

namespace Abstractions
{
    public interface IGameState
    {
        void AddFactionMember(IFactionMember factionMember, int instaceID);
        void RemoveFactionMember(IFactionMember factionMember, int instaceID);
        IObservable<FactionID> WhoWin { get; }
    }
}
