using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Model;
using UnityEngine;

namespace Asteroids.Logic
{
    public class Scoreboard
    {
        public int _score;
        public int Score => _score;

        public void AddScore()
        {
            _score++;
        }

        public void ResetScore()
        {
            _score = 0;
        }
    }
}
