using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Logic
{
    /// <summary>
    /// Abstract base class for FSMs
    /// </summary>
    public abstract class BaseFSM<T> : IFSM<T> where T : Enum
    {
        private BaseFSMState<T> currentState;
        private Dictionary<T, BaseFSMState<T>> states = new();
        private Queue<T> transitions = new();

        public event Action<T> OnState;

        public void AddState(T stateType, BaseFSMState<T> state)
        {
            if (!states.ContainsKey(stateType))
            {
                state.Init(this);
                states.Add(stateType, state);
            }
        }

        public void ChangeState(T newState)
        {
            transitions.Enqueue(newState);
        }

        protected void UpdateStates(float dt)
        {
            while (transitions.Count > 0)
            {
                var stateType = transitions.Dequeue();
                states.TryGetValue(stateType, out BaseFSMState<T> state);

                if (state == currentState)
                    continue;

                if (state != null)
                {
                    currentState?.OnExit();
                    currentState = state;
                    currentState.OnEnter();
                    OnState?.Invoke(stateType);
                }

                break;
            }

            transitions.Clear();
            currentState.OnUpdate(dt);
        }
    }

}

