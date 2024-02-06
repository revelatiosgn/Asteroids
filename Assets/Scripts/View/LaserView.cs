using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Model;
using UnityEngine;

namespace Asteroids.View
{
    public class LaserView : MonoBehaviour
    {
        [SerializeField] private GameObject _laser;

        private LaserGun _laserGun;

        public void Init(LaserGun laserGun)
        {
            _laserGun = laserGun;
        }

        private void FixedUpdate()
        {
            if (_laserGun == null)
                return;

            if (_laserGun.IsFiring)
                Fire();
            else
                StopFire();
        }

        public void Fire()
        {
            Debug.Log("Fire Laser");
            
            _laser.gameObject.SetActive(true);

            Ray ray = new Ray(_laserGun.Position, _laserGun.Forward.normalized);
            LayerMask layerMask = LayerMask.GetMask("Enemy");

            var hits = Physics2D.RaycastAll(ray.origin, ray.direction, float.MaxValue, layerMask);
            foreach (var hitInfo in hits)
            {
                if (hitInfo.collider.TryGetComponent<MovingView>(out var baseView) && baseView.MovingObject != null)
                {
                    baseView.MovingObject.TakeDamage();
                }
            }
        }

        public void StopFire()
        {
            _laser.gameObject.SetActive(false);
        }
    }
}

