using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public interface IState
    {
        void OnEnter ();
        void Run();
        void OnExit ();
    }
}
