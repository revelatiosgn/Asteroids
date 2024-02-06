using System.Collections;
using System.Collections.Generic;
using Asteroids.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UiController
{
    public class LaserDelay : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private LaserGun _laserGun;
        private GameSettings _gameSettings;
        
        public void Init(LaserGun laserGun, GameSettings gameSettings)
        {
            _laserGun = laserGun;
            _laserGun.OnRelease += () => _laserGun = null;

            _gameSettings = gameSettings;
        }

        public void Update()
        {
            if (_laserGun == null)
                return;

            _slider.value = _laserGun.FireDelay / _gameSettings.LaserGun.FireDelay;
        }
    }
}
