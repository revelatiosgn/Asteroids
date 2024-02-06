using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Model
{
    public interface IUpdatable
    {
        void Update(float dt);
    }
}
