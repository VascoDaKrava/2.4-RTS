using Abstractions;
using Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Core
{
    public class GameState : MonoBehaviour, IGameState
    {
        private Dictionary<FactionID, List<IFactionMember>> _factions;
        private Subject<FactionID> _whoWin = new Subject<FactionID>();
        private FactionID _winner = FactionID.Nobody;
        private bool _gameFinish = false;

        private void Awake()
        {
            InitializeFactions();
        }

        private void Start()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ => ThreadPool.QueueUserWorkItem(CheckState))
                .AddTo(this);
        }

        private void InitializeFactions()
        {
            FactionID[] factions = (FactionID[])Enum.GetValues(typeof(FactionID));
            _factions = new Dictionary<FactionID, List<IFactionMember>>(factions.Length);

            foreach (var faction in factions)
            {
                _factions.Add(faction, new List<IFactionMember>());
            }
        }

        private void CheckState(object state)
        {
            lock (_factions)
            {
                _gameFinish = true;

                foreach (var faction in _factions)
                {
                    if (faction.Value.Count != 0)
                    {
                        if (_winner == FactionID.Nobody)
                        {
                            _winner = faction.Key;
                        }
                        else
                        {
                            _winner = FactionID.Nobody;
                            _gameFinish = false;
                            break;
                        }
                    }
                }

                if (_gameFinish)
                {
                    _whoWin.OnNext(_winner);
                }
            }
        }

        public IObservable<FactionID> WhoWin => _whoWin;

        public void AddFactionMember(IFactionMember factionMember)
        {
            Debug.Log($"Add {factionMember.FactionID} / {factionMember}");
            lock (_factions)
            {
                _factions[factionMember.FactionID].Add(factionMember);
            }
        }

        public void RemoveFactionMember(IFactionMember factionMember)
        {
            Debug.Log($"Remove {factionMember.FactionID} / {factionMember}");
            lock (_factions)
            {
                _factions[factionMember.FactionID].Remove(factionMember);
            }
        }
    }
}