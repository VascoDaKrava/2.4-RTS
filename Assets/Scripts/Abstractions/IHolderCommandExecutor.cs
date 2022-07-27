using Abstractions.Commands;

namespace Abstractions
{
    public interface IHolderCommandExecutor
    {
        ICommand CurrentCommand { get; set; }
    }
}