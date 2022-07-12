using Abstractions.Commands;

namespace Abstractions
{
    public interface IHolderCommandQueue
    {
        void Enqueue(ICommand command);
        void ClearQueue();
    }
}
