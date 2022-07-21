namespace Abstractions
{
    public interface IUnitProductionTask : IHolderIcon, IHolderName
    {
    	float TimeLeft { get; }
    	float ProductionTime { get; }
    }
}