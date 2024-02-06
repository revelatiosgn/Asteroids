using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Model;
using UnityEngine;

namespace Asteroids.Logic
{
    public class GameplayFSM : BaseFSM<GameState>, IUpdatable
    {
        public void Update(float dt)
        {
            UpdateStates(dt);
        }
    }
}

