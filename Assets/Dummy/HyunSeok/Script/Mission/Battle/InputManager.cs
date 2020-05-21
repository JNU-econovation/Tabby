using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

namespace Battle
{
    public class InputManager : MonoBehaviour
    {
        Vector3 beginPoint;
        Vector3 endPoint;

        // FSM을 구동할 HeadMachine
        protected HeadMachine<InputManager> stateControl;
        // 상태 보유 리스트
        private IState[] states = new IState[(int) EInputState.END];
        // 현재 상태
        [SerializeField]
        private EInputState inputState;
        public EInputState InputState { get => inputState; set => inputState = value; }
        private void InitFSM ()
        {
            // 기본 상태
            InputState = EInputState.IDLE;
            // State 생성
            states[(int) EInputState.IDLE] = new IdleState (this);
            states[(int) EInputState.DRAG] = new DragState (this);
            // HeadMachine 생성
            stateControl = new HeadMachine<InputManager> (states[(int) InputState]);
        }

        private void MoveTargetToPoint()
        {
            
        }

        class IdleState : IState
        {
            private InputManager owner;
            public IdleState (InputManager owner) => this.owner = owner;
            public void OnEnter ()
            {
                owner.InputState = EInputState.IDLE;
            }

            public void OnExit () { }

            public void Run () { }
        }
        class DragState : IState
        {
            private InputManager owner;
            public DragState (InputManager owner) => this.owner = owner;
            public void OnEnter ()
            {
                owner.InputState = EInputState.IDLE;
                owner.beginPoint = BattleManager._instance.CameraControl.MainCam.ScreenToWorldPoint (Input.mousePosition);
            }

            public void OnExit ()
            {
                owner.endPoint = BattleManager._instance.CameraControl.MainCam.ScreenToWorldPoint (Input.mousePosition);
                owner.stateControl.Revert ();
            }

            public void Run () { }
        }

    }

}
