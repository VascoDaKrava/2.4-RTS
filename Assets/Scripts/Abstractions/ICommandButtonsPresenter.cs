using UniRx;

namespace Abstractions
{
    public interface ICommandButtonsPresenter
    {
        Subject<bool> IsCommandPending { get; }
    }
}