using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Model;
using Asteroids.View;
using UnityEngine;

namespace Asteroids.Logic
{
    public class PoolManager : IDisposable
    {
        private Dictionary<string, object> _objectPools = new Dictionary<string, object>();
        private Dictionary<ObjectType, ViewPool> _viewPools = new Dictionary<ObjectType, ViewPool>();

        private static Dictionary<ObjectType, string> _prefabs = new Dictionary<ObjectType, string>
        {
            { ObjectType.Asteroid, "Prefabs/Gameplay/Asteroid" },
            { ObjectType.Bullet, "Prefabs/Gameplay/Bullet" },
            { ObjectType.Ship, "Prefabs/Gameplay/Ship" },
            { ObjectType.UFO, "Prefabs/Gameplay/UFO" }
        };

        public ObjectPool<T> GetObjectPool<T>() where T : BaseObject, new()
        {
            string key = typeof(T).ToString();

            if (_objectPools.TryGetValue(key, out object pool))
            {
                return _objectPools[key] as ObjectPool<T>;
            }
            else
            {
                var objectPool = new ObjectPool<T>();
                _objectPools.Add(key, objectPool);

                return objectPool;
            }
        }

        public ViewPool GetViewPool(ObjectType objectType)
        {
            if (_viewPools.TryGetValue(objectType, out ViewPool pool))
            {
                return pool;
            }
            else
            {
                var pref = _prefabs[objectType];
                var res = Resources.Load(pref);

                var viewPool = new ViewPool();
                viewPool.Init(Resources.Load<MovingView>(_prefabs[objectType]));
                _viewPools.Add(objectType, viewPool);

                return viewPool;
            }
        }

        public void Dispose()
        {
            foreach (var pool in _objectPools.Values)
            {
                (pool as IDisposable).Dispose();
            }

            foreach (var pool in _viewPools.Values)
            {
                (pool as IDisposable).Dispose();
            }
        }
    }
}
