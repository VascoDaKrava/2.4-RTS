using Abstractions;
using Abstractions.Commands;
using UniRx;
using UnityEngine;

public sealed class QueueCommandsExecutor : MonoBehaviour, IHolderCommandQueue
{
    private ReactiveCollection<ICommand> _queue = new ReactiveCollection<ICommand>();

    private void Start()
    {
        
    }

    public void ClearQueue()
    {
        _queue.Clear();
    }

    public void Enqueue(ICommand command)
    {
        _queue.Add(command);
    }

    

}
