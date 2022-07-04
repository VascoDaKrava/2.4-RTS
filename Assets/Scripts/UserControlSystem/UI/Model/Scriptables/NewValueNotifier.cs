using Abstractions;
using UniRx;

namespace UserControlSystem
{
    public sealed class NewValueNotifier<TAwaited> : AwaiterBase<TAwaited>
    {
        public NewValueNotifier(ScriptableBase<TAwaited> scriptableValue)
        {
            scriptableValue
                .Subscribe(value => OnFinish(value));
        }
    }
}
