namespace Abstractions
{
    public interface IDamagable : IHolderHealth
    {
        void GetDamage(float value);
    }
}