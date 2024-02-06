using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Model;
using Asteroids.View;
using UnityEngine;

namespace Asteroids.Logic
{
    public class ObjectFactory
    {
        private Updater _updater;
        private GameSettings _gameSettings;
        private PoolManager _poolManager;
        private Scoreboard _scoreboard;

        public ObjectFactory(Updater updater, GameSettings gameSettings, PoolManager poolManager, Scoreboard scoreboard)
        {
            _updater = updater;
            _gameSettings = gameSettings;
            _poolManager = poolManager;
            _scoreboard = scoreboard;
        }

        public BaseObject Create(ObjectType objectType, Vector2 position, Vector2 velocity, float rotation)
        {
            switch (objectType)
            {
                case ObjectType.Asteroid:
                {
                    var obj = _poolManager.GetObjectPool<Asteroid>().Pull();
                    obj.Init(_gameSettings, position, velocity, rotation);
                    _updater.Add(obj);
                    obj.OnRelease += () => _updater.Remove(obj);
                    obj.OnRelease += () => _scoreboard.AddScore();

                    var view = _poolManager.GetViewPool(ObjectType.Asteroid).Pull();
                    view.Init(obj);

                    var asteroidView = view.GetComponent<AsteroidView>();
                    asteroidView.Init(obj);

                    return obj;
                }

                case ObjectType.Bullet:
                {
                    var obj = _poolManager.GetObjectPool<Bullet>().Pull();
                    obj.Init(_gameSettings, position, velocity, rotation);
                    _updater.Add(obj);
                    obj.OnRelease += () => _updater.Remove(obj);

                    var view = _poolManager.GetViewPool(ObjectType.Bullet).Pull();
                    view.Init(obj);

                    return obj;
                }

                case ObjectType.Ship:
                {
                    var obj = _poolManager.GetObjectPool<Ship>().Pull();
                    obj.Init(_gameSettings, position, velocity, rotation);
                    _updater.Add(obj);
                    obj.OnRelease += () => _updater.Remove(obj);

                    var view = _poolManager.GetViewPool(ObjectType.Ship).Pull();
                    view.Init(obj);

                    var shipView = view.GetComponent<ShipView>();
                    shipView.Init(obj);

                    var laserGun = _poolManager.GetObjectPool<LaserGun>().Pull();
                    laserGun.Init(_gameSettings);
                    _updater.Add(laserGun);
                    laserGun.OnRelease += () => _updater.Remove(laserGun);
                    obj.SetLaserGun(laserGun);

                    var laserView = view.GetComponent<LaserView>();
                    laserView.Init(laserGun);

                    obj.OnRelease += () => laserGun.Release();

                    return obj;
                }

                case ObjectType.UFO:
                {
                    var obj = _poolManager.GetObjectPool<UFO>().Pull();
                    obj.Init(_gameSettings, position, velocity, rotation);
                    _updater.Add(obj);
                    obj.OnRelease += () => _updater.Remove(obj);
                    obj.OnRelease += () => _scoreboard.AddScore();

                    var view = _poolManager.GetViewPool(ObjectType.UFO).Pull();
                    view.Init(obj);

                    return obj;
                }
            }

            return null;
        }

        public T Create<T>(ObjectType objectType) where T : BaseObject
        {
            return Create(objectType, Vector2.zero, Vector2.zero, 0.0f) as T;
        }

        public BaseObject Create(ObjectType objectType)
        {
            return Create(objectType, Vector2.zero, Vector2.zero, 0.0f);
        }

        public T Create<T>(ObjectType objectType, Vector2 position, Vector2 velocity, float rotation) where T : BaseObject
        {
            return Create(objectType, position, velocity, rotation) as T;
        }
    }
}
