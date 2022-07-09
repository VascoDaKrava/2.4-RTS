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
                .Subscribe(_ => InputFinish());

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
                Debug.Log($"Add {value}");
                _com.Enqueue(value);
            }
            else
            {
                Debug.Log($"Add {value}");
                _com.Enqueue(value);
                InputFinish();
            }
        }

        private void InputFinish()
        {
            Debug.Log("Input finish");

            foreach (var item in _com)
            {
                Debug.Log($"Point: {item}");
            }

            if (_com.Count == 0)
            {
                Debug.Log("Queue is empty");
                return;
            }

            Debug.Log($"Execute : {_com.Peek()}");
            OnFinish(_com.Dequeue());
            _streamClick.Dispose();
            _streamFinishInput.Dispose();
            _streamStillInput.Dispose();
        }
    }
}
