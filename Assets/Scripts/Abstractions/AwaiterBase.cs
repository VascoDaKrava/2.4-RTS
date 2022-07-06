using System;

namespace Abstractions
{
    public abstract class AwaiterBase<T> : IAwaiter<T>
    {
        private T _result;
        private Action _callback;
        private bool _isCompleted;

        public bool IsCompleted => _isCompleted;

        public T GetResult() => _result;

        protected void OnFinish(T result)
        {
            _result = result;
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