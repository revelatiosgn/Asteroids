using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Model
{
    public class UFO : MovingObject
    {
        private Ship _target;
        private UFOSetting _ufoSetting;

        public override void Init(GameSettings gameSettings, Vector2 position, Vector2 velocity, float rotation)
        {
            base.Init(gameSettings, position, velocity, rotation);

            _ufoSetting = gameSettings.UFO;
        }

        public void SetTarget(Ship target)
        {
            _target = target;
            _target.OnRelease += () => _target = null;
        }

        public override void Release()
        {
            base.Release();
            
            _target = null;
        }

        public override void Update(float dt)
        {
            if (_target != null)
            {
                Vector2 direction = _target.Position - Position;
                _veloctiy = direction.normalized * _ufoSetting.Speed;
            }

            base.Update(dt);
        }

        public override void TakeDamage()
        {
            Release();
        }

        public override void Collide(ICollidable other)
        {
            var damagable = other as IDamagable;
            damagable?.TakeDamage();
        }
    }
}
