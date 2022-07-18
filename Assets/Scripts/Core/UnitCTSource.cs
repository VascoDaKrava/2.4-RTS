using System.Threading;
using UnityEngine;

namespace Core
{
    public sealed class UnitCTSource : MonoBehaviour
    {
        private CancellationTokenSource _ctSource;

        public CancellationTokenSource CTSource { get => _ctSource; set => _ctSource = value; }
    }
}