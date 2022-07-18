using Abstractions;
using UnityEngine;

namespace Core
{
    public class FactionMember : MonoBehaviour, IFactionMember
    {
        [SerializeField] private int _factionID;
        
        public int FactionID => _factionID;

        public void SetFaction(int factionID)
        {
            _factionID = factionID;
        }
    }
}