using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Model
{
    public class LaserGun : BaseObject, IUpdatable
    {
        private bool _isFiring = false;
        public bool IsFiring => _isFiring;

        public Vector2 Position = Vector2.zero;
        public Vector2 Forward = Vector2.one.normalized;

        private float _fireDelay;
        public float FireDelay => _fireDelay;

        private float _fireDuration;
        private float _rechargeDelay;

        private int _charges;
        public int Charges => _charges;

        private GameSettings _gameSettings;

        public void Init(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _charges = _gameSettings.LaserGun.MaxCharges;

            _isFiring = false;
            _fireDelay = 0f;
            _fireDuration = 0f;
            _rechargeDelay = 0f;
        }

        public void TryFire()
        {
            if (_charges <= 0)
                return;

            if (_fireDelay > 0)
                return;

            _fireDelay = _gameSettings.LaserGun.FireDelay;
            _charges--;

            _isFiring = true;
        }

        public void Update(float dt)
        {
            if (_fireDelay > 0 && !_isFiring)
                _fireDelay -= dt;

            if (_isFiring)
            {
                _fireDuration += dt;

                if (_fireDuration >= _gameSettings.LaserGun.FireDuration)
                {
                    _fireDuration = 0;
                    _isFiring = false;
                }
            }

            if (_rechargeDelay > 0)
            {
                _rechargeDelay -= dt;
            }
            else if (_charges < _gameSettings.LaserGun.MaxCharges)
            {
                _charges++;
                _rechargeDelay = _gameSettings.LaserGun.RechargeDelay;
            }   
        }
    }
}
