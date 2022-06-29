using Abstractions;


namespace UserControlSystem
{
    public sealed class NewValueNotifier<TAwaited> : AwaiterBase<TAwaited>
    {
        private readonly ScriptableBase<TAwaited> _scriptableValue;

        public NewValueNotifier(ScriptableBase<TAwaited> scriptableValue)
        {
            _scriptableValue = scriptableValue;
            _scriptableValue.OnNewValue += ONNewValue;
        }

        private void ONNewValue(TAwaited obj)
        {
            _scriptableValue.OnNewValue -= ONNewValue;
            OnFinish(obj);
        }
    }
}
