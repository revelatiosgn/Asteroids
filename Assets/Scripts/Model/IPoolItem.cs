using System;

namespace Asteroids.Model
{
    public interface IPoolItem
    {
        event Action OnPoolPush;
        bool IsActive { get; }
        void SetActive(bool value);
        void Reset();
        void Destroy();
    }
}
