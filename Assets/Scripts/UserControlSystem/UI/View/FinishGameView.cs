using Abstractions;
using Abstractions.Enums;
using TMPro;
using UnityEngine;

namespace UserControlSystem.UI.View
{
    public class FinishGameView : MonoBehaviour, IFinishGameView
    {
        [SerializeField] private TextMeshProUGUI _text;

        public bool ShowMessage { set => gameObject.SetActive(value); }

        public void SetWin(FactionID factionID)
        {
            _text.text = $"Game over.\n" +
                $"Faction {factionID}\n" +
                $"has lost\n" +
                $"ALL units and buildings";
        }
    }
}