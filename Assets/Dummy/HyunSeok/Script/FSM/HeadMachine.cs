using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class HeadMachine<T>
    {
        // 이전 상태
        private IState prevState;
        // 현재 상태
        private IState currentState;
        // 생성자 초기화
        public HeadMachine (IState initState)
        {
            currentState = initState;
            prevState = initState;
        }
        /**
         *   HeadMachine 시동
         *   @param initState    작동시 초기화 할 상태
         */
        public void Begin (IState initState)
        {
            if (currentState != null)
            {
                currentState.OnEnter ();
            }
        }
        /**
         *   지속적으로 작업 수행 
         */
        public void Run ()
        {
            if (currentState != null)
            {
                currentState.Run ();
            }
        }
        /**
         *   이전 상태로 되돌림 
         */
        public void Revert ()
        {
            if (prevState != null)
                SetState (prevState);
        }
        /**
         *  HeadMachine 작동 중지
         */
        public void Stop ()
        {

            currentState.OnExit ();
            currentState = null;
            prevState = null;
        }
        /**
         *  HeadMachine 상태 변경
         *  @param nextState    변경할 상태
         */
        public void SetState (IState nextState)
        {
            if (currentState == nextState)
                return;
            if (currentState != null)
            {
                currentState.OnExit ();
            }
            currentState = nextState;
            currentState.OnEnter ();
        }

    }

}
