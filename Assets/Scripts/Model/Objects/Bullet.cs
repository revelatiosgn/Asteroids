using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Model
{
    public class Bullet : MovingObject
    {
        public override void Init(GameSettings gameSettings, Vector2 position, Vector2 velocity, float rotation)
        {
            base.Init(gameSettings, position, velocity, rotation);

            _warpOnBounds = false;
        }

        public override void Collide(ICollidable other)
        {
            var damagable = other as IDamagable;
            damagable?.TakeDamage();
            
            Release();
        }
    }
}
