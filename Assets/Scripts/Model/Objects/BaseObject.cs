using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Model
{
    public abstract class BaseObject : IPoolItem
    {
        protected bool _isActive = true;
        public bool IsActive => _isActive;

        public event Action OnRelease;
        public event Action OnPoolPush;

        public void SetActive(bool active)
        {
            _isActive = active;
        }

        public virtual void Reset()
        {
        }

        public virtual void Release()
        {
            OnRelease?.Invoke();
            
            OnRelease = null;

            OnPoolPush?.Invoke();
        }

        public virtual void Destroy()
        {
        }
    }
}
