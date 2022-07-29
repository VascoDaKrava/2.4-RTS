namespace Abstractions
{
    public interface IAttacker
    {
        float AttackStrength { get; }
        float AttackRange { get; }
        int AttackPeriod { get; }
    }
}