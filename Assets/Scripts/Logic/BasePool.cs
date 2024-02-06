using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Model;
using Asteroids.View;
using UnityEngine;

namespace Asteroids.Logic
{
    public abstract class BasePool<T> : IPool<T>, IDisposable where T : IPoolItem
    {
        private List<T> _pool = new List<T>();
        public int Capacity => _pool.Count;

        protected abstract T CreateItem();

        public T Pull()
        {
            foreach (var item in _pool)
            {
                if (!item.IsActive)
                {
                    item.SetActive(true);
                    return item;
                }
            }

            var newItem = CreateItem();
            newItem.OnPoolPush += () => Push(newItem);
            _pool.Add(newItem);

            return newItem;
        }

        public void Push(T item)
        {
            item.Reset();
            item.SetActive(false);
        }

        public void Flush()
        {
            foreach (var item in _pool)
            {
                if (item.IsActive)
                {
                    item.Reset();
                    item.SetActive(false);
                }
            }
        }

        public void Clear()
        {
            foreach (var item in _pool)
            {
                item.Reset();
                item.Destroy();
            }
        }

        public void Dispose()
        {
            Flush();
        }
    }
}
