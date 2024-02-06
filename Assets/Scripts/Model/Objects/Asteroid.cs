using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Model
{
    public class Asteroid : MovingObject
    {
        private int _level;
        public int Level => _level;

        public event Action<int> OnLevel;
        public event Action OnSplit;

        public override void Init(GameSettings gameSettings, Vector2 position, Vector2 velocity, float rotation)
        {
            base.Init(gameSettings, position, velocity, rotation);

            _level = gameSettings.Asteroid.StartLevel;
        }

        public override void TakeDamage()
        {
            Release();
        }

        public override void Release()
        {
            base.Release();

            OnLevel = null;

            OnSplit?.Invoke();
            OnSplit = null;
        }

        public void SetLevel(int level)
        {
            _level = Mathf.Max(1, level);
            OnLevel?.Invoke(_level);
        }

        public override void Collide(ICollidable other)
        {
            var damagable = other as IDamagable;
            damagable?.TakeDamage();
        }
    }
}
