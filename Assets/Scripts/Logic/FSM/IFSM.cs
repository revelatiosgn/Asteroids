using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Logic
{
    public interface IFSM<T> where T : Enum
    {
        void ChangeState(T newState);
    }
}

