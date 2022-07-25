using Abstractions;
using Abstractions.Enums;
using System;
using UniRx;
using UnityEngine;

namespace Core
{
    public class GameState : MonoBehaviour, IGameState
    {
        private Subject<FactionID> _whoWin = new Subject<FactionID>();

        public IObservable<FactionID> WhoWin => _whoWin;

        public void AddFactionMember(IFactionMember factionMember, int instaceID)
        {
            Debug.Log($"Add {factionMember.FactionID} / {instaceID}");
        }

        public void RemoveFactionMember(IFactionMember factionMember, int instaceID)
        {
            Debug.Log($"Remove {factionMember.FactionID} / {instaceID}");
        }
    }
}