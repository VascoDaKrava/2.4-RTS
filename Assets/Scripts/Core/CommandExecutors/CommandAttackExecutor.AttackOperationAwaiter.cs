using Abstractions;

namespace Core.CommandExecutors
{
    public partial class CommandAttackExecutor
    {
        public sealed partial class AttackOperation
        {
            public class AttackOperationAwaiter : AwaiterBase<AsyncExtensions.Void>
            {
                private AttackOperation _attackOperation;

                public AttackOperationAwaiter(AttackOperation attackOperation)
                {
                    _attackOperation = attackOperation;
                    attackOperation.OnComplete += OnComplete;
                }

                private void OnComplete()
                {
                    _attackOperation.OnComplete -= OnComplete;
                    OnFinish(new AsyncExtensions.Void());
                }
            }
        }
    }
}