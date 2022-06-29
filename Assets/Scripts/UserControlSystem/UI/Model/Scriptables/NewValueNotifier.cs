using Abstractions;
using System;


namespace UserControlSystem
{
    public sealed class NewValueNotifier<TAwaited> : IAwaiter<TAwaited>
    {
        private readonly ScriptableBase<TAwaited> _scriptableValue;
        private TAwaited _result;
        private Action _callback;
        private bool _isCompleted;

        public bool IsCompleted => _isCompleted;
        public TAwaited GetResult() => _result;

        public NewValueNotifier(ScriptableBase<TAwaited> scriptableValue)
        {
            _scriptableValue = scriptableValue;
            _scriptableValue.OnNewValue += ONNewValue;
        }

        private void ONNewValue(TAwaited obj)
        {
            _scriptableValue.OnNewValue -= ONNewValue;
            _result = obj;
            _isCompleted = true;
            _callback?.Invoke();
        }

        public void OnCompleted(Action callback)
        {
            if (_isCompleted)
            {
                callback?.Invoke();
            }
            else
            {
                _callback = callback;
            }
        }
    }
}
