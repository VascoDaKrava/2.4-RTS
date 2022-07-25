using Abstractions;
using Abstractions.Enums;
using UnityEngine;
using Zenject;

namespace Core
{
    public class FactionMember : MonoBehaviour, IFactionMember
    {
        [SerializeField] private FactionID _factionID;

        [Inject] private IGameState _gameState;

        public FactionID FactionID => _factionID;

        public void SetFaction(FactionID factionID)
        {
            _factionID = factionID;
            _gameState.AddFactionMember(this, GetInstanceID());
        }

        private void Awake()
        {
            if (_factionID != FactionID.Nobody)
            {
                SetFaction(_factionID);
            }
        }

        private void OnDestroy()
        {
            _gameState.RemoveFactionMember(this, GetInstanceID());
        }
    }
}