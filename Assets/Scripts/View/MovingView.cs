using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Model;
using UnityEngine;
using UnityEngine.Assertions;

namespace Asteroids.View
{
    public class MovingView : MonoBehaviour, IPoolItem
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        protected MovingObject _movingObject;
        public MovingObject MovingObject => _movingObject;

        public bool IsActive => gameObject.activeSelf;

        public event Action OnRelease;
        public event Action OnPoolPush;

        public virtual void Init(MovingObject movingObject)
        {
            _movingObject = movingObject;

            transform.position = _movingObject.Position;
            transform.rotation = Quaternion.Euler(0, 0, _movingObject.Rotation);
            transform.localScale = Vector3.one;

            _movingObject.OnWarp += OnWarp;
            _movingObject.OnRelease += OnObjectRelease;
        }

        public virtual void Reset()
        {
            _movingObject = null;
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_movingObject.Position);
            _rigidbody.MoveRotation(_movingObject.Rotation);
        }

        private void OnObjectRelease()
        {
            OnRelease?.Invoke();

            OnRelease = null;

            OnPoolPush?.Invoke();
        }

        private void OnWarp()
        {
            _rigidbody.position = _movingObject.Position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_movingObject != null && other.TryGetComponent(out MovingView otherView) && otherView.MovingObject != null)
            {
                _movingObject.Collide(otherView.MovingObject);
            }
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}

