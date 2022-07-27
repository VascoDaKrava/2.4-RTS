using System.Threading;
using UnityEngine;

namespace Core
{
    public sealed class UnitCTSource : MonoBehaviour
    {
        private CancellationTokenSource _ctSource;

        public CancellationToken Token => _ctSource == default ? default : _ctSource.Token;

        public void NewToken() => _ctSource = new CancellationTokenSource();

        public void ClearToken()
        {
            Cancel();
            _ctSource = null;
        }

        public void Cancel() => _ctSource?.Cancel();
    }
}