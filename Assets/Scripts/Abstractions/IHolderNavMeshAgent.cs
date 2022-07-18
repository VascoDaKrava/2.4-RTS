using UnityEngine.AI;

namespace Abstractions
{
    public interface IHolderNavMeshAgent
    { 
        public abstract NavMeshAgent NavMeshAgent { get; }
    }
}