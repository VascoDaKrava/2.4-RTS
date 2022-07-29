using Abstractions;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace UserControlSystem
{
    public sealed class NewValueNotifier<TAwaited> : AwaiterBase<TAwaited>
    {
        private IDisposable _streamClick;
        private IDisposable _streamFinishInput;
        private IDisposable _streamStillInput;
        private bool _shiftStillPress;
        private Queue<TAwaited> _com = new Queue<TAwaited>();

        public NewValueNotifier(ScriptableBase<TAwaited> scriptableValue)
        {
            _streamFinishInput = Observable
                .EveryUpdate()
                .Where(_ => Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
                .Subscribe(_ =>
                {
                    _shiftStillPress = false;
                    InputFinish();
                });

            _streamStillInput = Observable
                .EveryUpdate()
                .Where(_ => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                .Subscribe(_ => _shiftStillPress = true);

            _streamClick = scriptableValue
                .Skip(1)
                .Subscribe(value => onCatch(value));
        }

        private void onCatch(TAwaited value)
        {
            if (_shiftStillPress)
            {
                _com.Enqueue(value);
            }
            else
            {
                _com.Enqueue(value);
                InputFinish();
            }
        }

        private void InputFinish()
        {
            if (_com.Count == 0)
            {
                return;
            }

            _streamClick.Dispose();
            _streamFinishInput.Dispose();
            _streamStillInput.Dispose();
            OnFinish(_com.ToArray());
        }
    }
}
