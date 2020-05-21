using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

namespace Battle
{
    public class CameraControl : MonoBehaviour
    {
        // FSM을 구동할 HeadMachine
        private HeadMachine<CameraControl> stateControl;
        // 상태 보유 리스트
        protected IState[] states = new IState[(int) ECameraState.END];
        // 현재 상태
        [SerializeField]
        protected ECameraState cameraState;
        public ECameraState CameraState { get => cameraState; set => cameraState = value; }
        // 카메라
        private Camera mainCam;
        public Camera MainCam { get => mainCam; set => mainCam = value; }
        // 카메라 추적 대상
        [SerializeField]
        private GameObject target;
        public GameObject Target {
            get => target; 
            set
            {
                if (value == null)
                {
                    stateControl.SetState(states[(int)ECameraState.IDLE]);
                }
                else
                {
                    target = value;
                    stateControl.SetState(states[(int)ECameraState.FOLLOW]);
                }

            }
        }

        void Awake ()
        {
            mainCam = GetComponent<Camera> ();
            InitFSM ();
        }

        void Start()
        {
            InitEvent ();
        }

        void LateUpdate ()
        {
            stateControl.Run ();
        }

        void InitFSM ()
        {
            // 기본 상태
            cameraState = ECameraState.IDLE;
            // State 생성
            states[(int) ECameraState.IDLE] = new IdleState (this);
            states[(int) ECameraState.FOLLOW] = new FollowState (this);
            // HeadMachine 생성
            stateControl = new HeadMachine<CameraControl> (states[(int) CameraState]);
        }

        void InitEvent ()
        {
            BattleManager._instance.EvTargetAnimalChange += new BattleManager.Event (() => Target = BattleManager._instance.TargetAnimal.gameObject);
            BattleManager._instance.EvTargetAnimalNull += new BattleManager.Event (() => Target = null);
        }

        class IdleState : IState
        {
            private CameraControl owner;
            public IdleState (CameraControl owner) => this.owner = owner;
            public void OnEnter ()
            {
                owner.CameraState = ECameraState.IDLE;
            }

            public void OnExit () { }

            public void Run () { }
        }

        class FollowState : IState
        {
            private CameraControl owner;
            public FollowState (CameraControl owner) => this.owner = owner;
            public void OnEnter ()
            {
                owner.CameraState = ECameraState.FOLLOW;
            }

            public void OnExit ()
            {

            }

            public void Run ()
            {
                owner.transform.position = Vector2.Lerp (owner.transform.position, owner.Target.transform.position, 2f * Time.deltaTime);
                owner.transform.position = new Vector3 (owner.transform.position.x, owner.transform.position.y, -10f);
            }
        }
    }
}
