using System;

namespace Abstractions
{
    public abstract class AwaiterBase<T> : IAwaiter<T>
    {
        private T[] _results;
        private Action _callback;
        private bool _isCompleted;

        public bool IsCompleted => _isCompleted;

        public T[] GetResult() => _results;

        protected void OnFinish(T result)
        {
            _results = new T[] { result };
            _isCompleted = true;
            _callback?.Invoke();
        }

        protected void OnFinish(T[] result)
        {
            _results = new T[result.Length];
            result.CopyTo(_results, 0);
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