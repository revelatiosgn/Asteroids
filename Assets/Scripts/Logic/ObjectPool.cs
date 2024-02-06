using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Model;
using Asteroids.View;
using UnityEngine;

namespace Asteroids.Logic
{
    public class ObjectPool<T> : BasePool<T> where T : BaseObject, new()
    {
        protected override T CreateItem()
        {
            return new T();
        }
    }
}
