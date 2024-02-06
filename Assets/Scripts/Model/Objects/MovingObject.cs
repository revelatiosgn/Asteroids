using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Model
{
    public abstract class MovingObject : BaseObject, IUpdatable, IDamagable, ICollidable
    {
        protected Vector2 _veloctiy;
        public Vector2 Velocity => _veloctiy;

        protected Vector2 _position;
        public Vector2 Position => _position;

        protected float _rotation;
        public float Rotation => _rotation;

        public Vector2 Forward => new Vector2(Mathf.Cos(_rotation * Mathf.Deg2Rad), Mathf.Sin(_rotation * Mathf.Deg2Rad));

        public event Action OnWarp;

        protected bool _warpOnBounds = true;
        public static Vector3 ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        public virtual void Init(GameSettings gameSettings, Vector2 position, Vector2 velocity, float rotation)
        {
            _position = position;
            _veloctiy = velocity;
            _rotation = rotation;

            OnWarp = null;
        }

        public virtual void Update(float dt)
        {
            if (!IsActive)
                return;

            _position += _veloctiy * dt;

            CheckBounds();
        }

        private void CheckBounds()
        {
            var warpPosition = _position;

            if (warpPosition.x > ScreenBounds.x)
                warpPosition.x = -ScreenBounds.x;
            else if (warpPosition.x < -ScreenBounds.x)
                warpPosition.x = ScreenBounds.x;

            if (warpPosition.y > ScreenBounds.y)
                warpPosition.y = -ScreenBounds.y;
            else if (warpPosition.y < -ScreenBounds.y)
                warpPosition.y = ScreenBounds.y;

            if (warpPosition != _position)
            {
                if (_warpOnBounds)
                {
                    _position = warpPosition;
                    OnWarp?.Invoke();
                }
                else
                {
                    Release();
                }
            }
        }

        public virtual void Collide(ICollidable other)
        {
        }

        public virtual void TakeDamage()
        {
        }
    }
}
