using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Model
{
    public class Ship : MovingObject
    {
        private ShipSettings _settings;

        private LaserGun _laserGun;
        public LaserGun LaserGun => _laserGun;

        public Action<bool> OnAccelerate;

        public override void Init(GameSettings gameSettings, Vector2 position, Vector2 velocity, float rotation)
        {
            base.Init(gameSettings, position, velocity, rotation);

            _settings = gameSettings.Ship;
        }

        public override void Release()
        {
            base.Release();

            OnAccelerate = null;
        }

        public void Rotate(float value)
        {
            _rotation += value * _settings.RotationSpeed;
            // _rotation = Mathf.Repeat(_rotation, 360);
            _rotation %= 360;
        }

        public void Accelerate(float value)
        {
            _veloctiy += Forward * _settings.Acceleration * value;
            _veloctiy = Vector2.ClampMagnitude(_veloctiy, _settings.MaxSpeed);

            OnAccelerate?.Invoke(value > 0);
        }

        public void SetLaserGun(LaserGun laserGun)
        {
            _laserGun = laserGun;
        }

        public override void Update(float dt)
        {
            base.Update(dt);

            if (_laserGun != null)
            {
                _laserGun.Position = _position;
                _laserGun.Forward = Forward;
            }
        }

        public override void TakeDamage()
        {   
            Release();
        }
    }
}
