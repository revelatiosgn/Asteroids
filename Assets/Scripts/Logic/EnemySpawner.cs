using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Model;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Asteroids.Logic
{
    public class EnemySpawner : IUpdatable
    {
        private ObjectFactory _objectFactory;
        private GameSettings _gameSettings;
        private ObjectLibrary _objectLibrary;

        private float _spawnTimer;

        public static Vector3 ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        public EnemySpawner(ObjectFactory objectFactory, GameSettings gameSettings, ObjectLibrary objectLibrary)
        {
            _objectFactory = objectFactory;
            _gameSettings = gameSettings;
            _objectLibrary = objectLibrary;

            _spawnTimer = _gameSettings.Spawn.MinSpawnTime;
        }

        public void Update(float dt)
        {
            _spawnTimer -= dt;
            while (_spawnTimer <= 0)
            {
                SpawnEnemy();
                _spawnTimer += Random.Range(_gameSettings.Spawn.MinSpawnTime, _gameSettings.Spawn.MaxSpawnTime);
            }
        }

        private void SpawnEnemy()
        {
            if (!_gameSettings.Spawn.SpawnEnemy)
                return;

            Vector2 shipPosition = _objectLibrary.Ship != null ? _objectLibrary.Ship.Position : new Vector2(0, 0);
            Vector2 position;

            position.x = -Mathf.Sign(shipPosition.x) * ScreenBounds.x;
            position.y = -Mathf.Sign(shipPosition.y) * ScreenBounds.y;

            if (Random.Range(0, 2) == 0)
                position.x += -Mathf.Sign(position.x) * Random.Range(0, ScreenBounds.x);
            else
                position.y += -Mathf.Sign(position.y) * Random.Range(0, ScreenBounds.y);

            var enemyType = GetRandomEnemyType();
            if (enemyType == ObjectType.Asteroid)
            {
                float min = Math.Min(ScreenBounds.x, ScreenBounds.y);
                Vector2 target = new Vector2(Random.Range(-min, min), Random.Range(-min, min));
                float speed = Random.Range(_gameSettings.Asteroid.MinSpeed, _gameSettings.Asteroid.MaxSpeed);

                var asteroid = _objectFactory.Create<Asteroid>(ObjectType.Asteroid, position, (target - position).normalized * speed, 0);
                asteroid.OnSplit += () => {
                    TrySplitAsteroid(asteroid);
                };
            }
            else
            {
                var ufo = _objectFactory.Create<UFO>(ObjectType.UFO, position, Vector2.zero, 0);
                if (_objectLibrary.Ship != null)
                    ufo.SetTarget(_objectLibrary.Ship);
            }
        }

        private ObjectType GetRandomEnemyType()
        {
            float totalWeight = 0;
            foreach (var weight in _gameSettings.Spawn.Weights)
                totalWeight += weight.Weight;

            float rnd = Random.Range(0, totalWeight);
            for (int i = 0; i < _gameSettings.Spawn.Weights.Count; i++)
            {
                if (rnd < _gameSettings.Spawn.Weights[i].Weight)
                    return _gameSettings.Spawn.Weights[i].Type;

                rnd -= _gameSettings.Spawn.Weights[i].Weight;
            }

            return ObjectType.Asteroid;
        }

        public void TrySplitAsteroid(Asteroid asteroid)
        {
            if (asteroid.Level > 0)
            {
                float speed = asteroid.Velocity.magnitude;
                for (int i = 0; i < _gameSettings.Asteroid.SplitCount; i++)
                {
                    Vector2 velocity = Random.insideUnitCircle.normalized * speed * _gameSettings.Asteroid.SplitAcceleration;
                    var newAsteroid = _objectFactory.Create<Asteroid>(ObjectType.Asteroid, asteroid.Position, velocity, 0);
                    newAsteroid.SetLevel(asteroid.Level - 1);
                }
            }
        }
    }
}
