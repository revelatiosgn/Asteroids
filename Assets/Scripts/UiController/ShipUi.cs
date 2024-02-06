using System.Collections;
using System.Collections.Generic;
using Asteroids.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UiController
{
    public class ShipUi : MonoBehaviour
    {
        [SerializeField] private TMP_Text _position;
        [SerializeField] private TMP_Text _angle;
        [SerializeField] private TMP_Text _speed;

        private Ship _ship;
        
        public void Init(Ship ship)
        {
            _ship = ship;
            _ship.OnRelease += () => _ship = null;
        }

        public void Update()
        {
            if (_ship == null)
                return;

            _position.text = $"{(int)_ship.Position.x} {(int)_ship.Position.y}";
            _angle.text = $"{(int)_ship.Rotation}°";
            _speed.text = $"{(int)(_ship.Velocity.magnitude * 1000f)} m/s";
        }
    }
}
