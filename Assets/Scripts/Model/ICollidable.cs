using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Model
{
    public interface ICollidable
    {
        void Collide(ICollidable other);
    }
}
