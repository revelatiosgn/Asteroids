using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Model;
using UnityEngine;

namespace Asteroids.Logic
{
    public class Updater
    {
        private List<IUpdatable> _updatables = new List<IUpdatable>();

        public void Add(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }

        public void Remove(IUpdatable updatable)
        {
            _updatables.Remove(updatable);
        }

        public void Update(float dt)
        {
            foreach (var updatable in _updatables.ToList())
            {
                updatable.Update(dt);
            }
        }
    }
}
