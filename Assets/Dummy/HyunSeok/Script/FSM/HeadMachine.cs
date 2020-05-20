using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class HeadMachine<T>
    {
        private IState prevState;
        private IState currentState;

        public HeadMachine (IState initState)
        {
            currentState = initState;
            prevState = initState;
        }

        public void Begin (IState initState)
        {
            if (currentState != null)
            {
                currentState.OnEnter();
            }
        }

        public void Run()
        {
            if (currentState != null)
            {
                currentState.Run();
            }
        }
        public void Revert()
        {
            if (prevState != null)
                SetState(prevState);
        }

        public void Stop ()
        {
            
            currentState.OnExit();
            currentState = null;
            prevState = null;
        }

        public void SetState (IState nextState)
        {
            if(currentState == nextState)
                return ;
            if (currentState != null)
            {
                currentState.OnExit ();
            }
            currentState = nextState;
            currentState.OnEnter ();
        }

    }

}
