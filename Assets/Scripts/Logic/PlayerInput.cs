using System.Collections;
using System.Collections.Generic;
using Asteroids.Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Logic
{
    public class PlayerInput : IUpdatable
    {
        private ObjectLibrary _objectLibrary;
        private GameSettings _gameSettings;
        private ObjectFactory _objectFactory;

        private InputAction _moveAction;
        private InputAction _fireAction;

        private float _bulletFireTimer;

        public PlayerInput(ObjectLibrary objectLibrary, GameSettings gameSettings, ObjectFactory objectFactory)
        {
            _objectLibrary = objectLibrary;
            _gameSettings = gameSettings;
            _objectFactory = objectFactory;

            _moveAction = _gameSettings.Ship.InputActions.actionMaps[0].FindAction("Move");
            _moveAction.Enable();

            _fireAction = _gameSettings.Ship.InputActions.actionMaps[0].FindAction("Fire");
            _fireAction.Enable();

            _bulletFireTimer = _gameSettings.Ship.BulletFireDelay;
        }

        public void Update(float dt)
        {
            UpdateShipInput(dt);
            UpdateBulletFire(dt);
        }

        private void UpdateShipInput(float dt)
        {
            if (_objectLibrary.Ship == null)
                return;

            Vector2 moveInput = _moveAction.ReadValue<Vector2>();
            _objectLibrary.Ship.Rotate(-moveInput.x * dt);
            _objectLibrary.Ship.Accelerate(moveInput.y > 0f ? moveInput.y * dt : 0f);

            _fireAction.performed += ctx => {
                if (_objectLibrary.Ship != null && _objectLibrary.Ship.LaserGun != null) 
                    _objectLibrary.Ship.LaserGun.TryFire();
            };
        }

        private void UpdateBulletFire(float dt)
        {
            if (_objectLibrary.Ship == null)
                return;

            Ship ship = _objectLibrary.Ship;

            _bulletFireTimer -= dt;
            while (_bulletFireTimer <= 0f)
            {
                _objectFactory.Create(
                    ObjectType.Bullet, 
                    ship.Position, 
                    ship.Velocity + ship.Forward * _gameSettings.Bullet.Speed,
                    ship.Rotation
                    );

                _bulletFireTimer += _gameSettings.Ship.BulletFireDelay;
            }
        }
    }
}
