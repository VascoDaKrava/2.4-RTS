namespace Abstractions
{
    public interface IAwaitable<T>
    {
        IAwaiter<T> GetAwaiter();
    }
}