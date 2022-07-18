namespace Abstractions
{
    public interface IUnitProductionTask : IHolderIcon, IHolderName
    {
    	public float TimeLeft { get; }
    	public float ProductionTime { get; }
    }
}