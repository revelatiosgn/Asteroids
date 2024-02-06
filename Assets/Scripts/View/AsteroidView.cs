using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Model;
using UnityEngine;

namespace Asteroids.View
{
    public class AsteroidView : MonoBehaviour
    {
        public void Init(Asteroid asteroid)
        {
            asteroid.OnLevel += OnLevel;
        }

        private void OnLevel(int level)
        {
            transform.localScale = level > 1 ? Vector3.one : Vector3.one * .5f;
        }
    }
}

