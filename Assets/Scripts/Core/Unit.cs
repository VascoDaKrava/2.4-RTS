using Abstractions;
using UnityEngine;


namespace Core
{
    public sealed class Unit : MonoBehaviour, ISelectable
    {

        #region Fields

        [SerializeField]
        private float _maxHealth = 100.0f;

        [SerializeField]
        private Sprite _icon;

        [SerializeField]
        private GameObject _selectionMarker;

        private float _health = 75.0f;

        #endregion


        #region Properties

        public float Health => _health;

        public float MaxHealth => _maxHealth;

        public Sprite Icon => _icon;

        public bool Selected
        {
            get => _selectionMarker.activeSelf;
            set => _selectionMarker.SetActive(value);
        }

        #endregion

    }
}