using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Logic
{
    /// <summary>
    /// Abstract base class for FSM States
    /// </summary>
    public abstract class BaseFSMState<T> where T : Enum
    {
        protected IFSM<T> _fsm;

        public virtual void Init(IFSM<T> fSM)
        {
            _fsm = fSM;
        }

        public virtual void OnEnter() {}
        public virtual void OnExit() {}
        public virtual void OnUpdate(float dt) {}
    }
}