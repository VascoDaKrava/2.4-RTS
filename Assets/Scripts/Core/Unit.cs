using Abstractions;
using UnityEngine;


namespace Core
{
    public sealed class Unit : MonoBehaviour, ISelectable, IAttackable
    {

        #region Fields

        [SerializeField] private float _attackStrength = 25.0f;
        [SerializeField] private float _maxHealth = 100.0f;
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _selectionMarker;

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

        public float AttackStrength => _attackStrength;

        public Vector3 Position { get => transform.position; set => transform.position = value; }

        #endregion

    }
}