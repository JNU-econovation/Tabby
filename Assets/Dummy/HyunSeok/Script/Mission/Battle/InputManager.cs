using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

namespace Battle
{
    public class InputManager : MonoBehaviour
    {
        #region Event
        public delegate void Event ();
        public delegate void EventEnemy (Enemy enemy);
        public delegate void EventAnimal (Animal animal);
        // Drag event
        public event Event EvDragBegin;
        public event Event EvDragEnd;
        public event Event EvDragging;
        // Click event
        public event EventAnimal EvClickAnimal;
        public event EventEnemy EvClickEnemy;
        
        #endregion
        private Vector3 beginPoint;
        public Vector3 BeginPoint { get => beginPoint; set => beginPoint = value; }
        private Vector3 endPoint;
        public Vector3 EndPoint { get => endPoint; set => beginPoint = endPoint; }
        #region FSM
        // FSM을 구동할 HeadMachine
        protected HeadMachine<InputManager> stateControl;
        // 상태 보유 리스트
        private IState[] states = new IState[(int) EInputState.END];
        // 현재 상태
        [SerializeField]
        private EInputState inputState;
        public EInputState InputState { get => inputState; set => inputState = value; }
        #endregion
        void Awake ()
        {
            InitFSM ();
            BattleManager._instance.InputManager = this;
        }
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

        void Update ()
        {
            CheckInput ();
            stateControl.Run ();
        }

        private void CheckInput ()
        {
            if (Input.GetMouseButtonDown (0))
            {
                RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    if (hit.transform.gameObject.CompareTag ("Animal"))
                    {
                        EvClickAnimal (hit.transform.gameObject.GetComponent<Animal>());
                    }
                    else if (hit.transform.gameObject.CompareTag ("Enemy"))
                    {
                        EvClickEnemy (hit.transform.gameObject.GetComponent<Enemy>());
                    }
                }
                else
                    stateControl.SetState (states[(int) EInputState.DRAG]);
            }
            if (Input.GetMouseButtonUp (0))
            {
                stateControl.SetState (states[(int) EInputState.IDLE]);
            }
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
                owner.InputState = EInputState.DRAG;
                owner.beginPoint = BattleManager._instance.CameraControl.MainCam.ScreenToWorldPoint (Input.mousePosition);
                owner.EvDragBegin ();
            }

            public void OnExit ()
            {
                owner.endPoint = BattleManager._instance.CameraControl.MainCam.ScreenToWorldPoint (Input.mousePosition);
                owner.EvDragEnd ();
            }

            public void Run ()
            {
                owner.endPoint = BattleManager._instance.CameraControl.MainCam.ScreenToWorldPoint (Input.mousePosition);
                owner.EvDragging ();
            }
        }
    }

}
