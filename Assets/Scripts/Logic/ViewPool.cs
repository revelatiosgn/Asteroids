using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Model;
using Asteroids.View;
using UnityEngine;

namespace Asteroids.Logic
{
    public class ViewPool : BasePool<MovingView>
    {
        private MovingView _prefab;
        private int _id = 0;

        public void Init(MovingView prefab)
        {
            _prefab = prefab;
        }

        protected override MovingView CreateItem()
        {
            var view = GameObject.Instantiate(_prefab);
            view.name += $"_{_id++}";

            return view;
        }
    }
}
