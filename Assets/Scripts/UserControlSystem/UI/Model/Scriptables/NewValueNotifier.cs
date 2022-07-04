using Abstractions;
using UniRx;

namespace UserControlSystem
{
    public sealed class NewValueNotifier<TAwaited> : AwaiterBase<TAwaited>
    {
        //private readonly ScriptableBase<TAwaited> _scriptableValue;

        public NewValueNotifier(ScriptableBase<TAwaited> scriptableValue)
        {
            //_scriptableValue = scriptableValue;
            //_scriptableValue.OnNewValue += OnNewValue;
            scriptableValue
                .Subscribe(value => OnFinish(value));
        }

        //private void OnNewValue(TAwaited obj)
        //{
        //    _scriptableValue.OnNewValue -= OnNewValue;
        //    OnFinish(obj);
        //}
    }
}
