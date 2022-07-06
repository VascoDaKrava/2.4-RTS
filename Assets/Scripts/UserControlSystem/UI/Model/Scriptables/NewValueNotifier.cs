using Abstractions;
using System;
using UniRx;

namespace UserControlSystem
{
    public sealed class NewValueNotifier<TAwaited> : AwaiterBase<TAwaited>
    {
        private IDisposable _stream;

        public NewValueNotifier(ScriptableBase<TAwaited> scriptableValue)
        {
            _stream = scriptableValue
                .Skip(1)
                .Subscribe(value => onCatch(value));
        }

        private void onCatch(TAwaited value)
        {
            OnFinish(value);
            _stream.Dispose();
        }
    }
}
