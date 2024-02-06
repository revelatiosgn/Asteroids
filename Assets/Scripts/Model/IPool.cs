namespace Asteroids.Model
{
    public interface IPool<T> where T : IPoolItem
    {
        T Pull();
        void Push(T item);
        void Flush();
        void Clear();
        int Capacity { get; }
    }
}
