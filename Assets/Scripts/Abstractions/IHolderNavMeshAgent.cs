using UnityEngine.AI;

namespace Abstractions
{
    public interface IHolderNavMeshAgent
    { 
        NavMeshAgent NavMeshAgent { get; }
    }
}