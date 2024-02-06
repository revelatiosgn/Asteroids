using System.Collections;
using System.Collections.Generic;
using Asteroids.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UiController
{
    public class LaserCharges : MonoBehaviour
    {
        [SerializeField] private TMP_Text _charges;

        private LaserGun _laserGun;
        
        public void Init(LaserGun laserGun)
        {
            _laserGun = laserGun;
            _laserGun.OnRelease += () => _laserGun = null;
        }

        public void Update()
        {
            if (_laserGun == null)
                return;

            _charges.text = _laserGun.Charges.ToString();
        }
    }
}
