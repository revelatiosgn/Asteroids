using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Model;
using UnityEngine;

namespace Asteroids.View
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField] private GameObject _acceleration;

        public void Init(Ship ship)
        {
            ship.OnAccelerate += OnAccelerate;
        }

        private void OnAccelerate(bool value)
        {
            _acceleration.SetActive(value);
        }
    }
}

