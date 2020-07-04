using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

namespace BattleDummy
{
    public class AttackCollision : MonoBehaviour
    {
        #region State
        private ActState actState;
        #endregion
        #region FSM
        // FSM을 구동할 HeadMachine
        private HeadMachine<AttackCollision> stateControl;
        // 상태 보유 리스트
        private IState[] states = new IState[(int) EAtkColState.END];
        // 현재 상태
        [SerializeField]
        private EAtkColState atkColState;
        public EAtkColState AtkColState { get => atkColState; set => atkColState = value; }
        #endregion
        #region TYPE
        [SerializeField]
        private EAtkColPoolSize poolSize;
        public EAtkColPoolSize PoolSize { get => poolSize; set => poolSize = value; }

        [SerializeField]
        private EAtkColDestroyType destroyType;
        public EAtkColDestroyType DestroyType { get => destroyType; set => destroyType = value; }

        [SerializeField]
        private EAtkColSprType sprType;
        public EAtkColSprType SprType { get => sprType; set => sprType = value; }
        #endregion
        // AttackCollision 정보, 리팩토링 할 것
        [SerializeField]
        private float damage;
        public float Damage { get => damage; set => damage = value; }

        [SerializeField]
        private float speed;
        public float Speed { get => speed; set => speed = value; }
        // 파괴 시 돌아가야 할 풀
        private AtkColPool atkColPool;
        // 추적 대상
        private Enemy target;
        // 스프라이트
        private SpriteRenderer spriteRenderer;

        private void Awake ()
        {
            InitFSM ();
            spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
        }
        void Update ()
        {
            stateControl.Run ();
        }
        public void Init (AtkColPool atkColPool)
        {
            gameObject.SetActive (false);
            this.atkColPool = atkColPool;
        }

        void InitFSM ()
        {
            // 기본 상태
            atkColState = EAtkColState.IDLE;
            // State 생성
            states[(int) EAtkColState.IDLE] = new IdleState (this);
            states[(int) EAtkColState.ACT] = new ActState (this);
            states[(int) EAtkColState.DIE] = new DieState (this);
            // HeadMachine 생성
            stateControl = new HeadMachine<AttackCollision> (states[(int) AtkColState]);
            // state 할당
            actState = states[(int) EAtkColState.ACT] as ActState;

        }
        public void Shot (Vector3 dir)
        {
            gameObject.SetActive (true);
            // 공격
            actState.dir = dir;
            stateControl.Begin (states[(int) EAtkColState.ACT]);
        }

        public void Shot (Enemy target)
        {
            gameObject.SetActive (true);
            // 공격
            this.target = target;
            stateControl.Begin (states[(int) EAtkColState.ACT]);
        }
        // 파괴 시 pool로 돌아가야함
        public void Destroyed ()
        {
            gameObject.SetActive (false);
            atkColPool.ReturnToPool (this);
        }

        class IdleState : IState
        {
            private AttackCollision owner;
            public IdleState (AttackCollision owner) => this.owner = owner;
            public void OnEnter ()
            {
                owner.AtkColState = EAtkColState.IDLE;
            }

            public void OnExit ()
            {

            }

            public void Run ()
            {

            }
        }

        class ActState : IState
        {
            public Vector2 dir;
            public Enemy target;
            private AttackCollision owner;
            public ActState (AttackCollision owner) => this.owner = owner;
            public void OnEnter ()
            {
                owner.AtkColState = EAtkColState.ACT;
                owner.transform.position =
                    owner.atkColPool.transform.position;
            }

            public void OnExit ()
            {

            }

            public void Run ()
            {
                // 움직이기
                switch (owner.destroyType)
                {
                    case EAtkColDestroyType.TARGET:
                        // 추적하기
                        Chase ();
                        break;
                    case EAtkColDestroyType.SOLID:
                        //
                        break;
                }
                // sprite 모양 변경
                switch (owner.sprType)
                {
                    case EAtkColSprType.FIX:
                        // 아마 아무것도 안해도 될듯
                        break;
                    case EAtkColSprType.DIR:
                        owner.spriteRenderer.gameObject.transform.rotation =
                            Quaternion.Euler (0, 0, Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg);
                        break;
                }
                if (IsAtkRange ())
                {
                    owner.stateControl.SetState (owner.states[(int) EAtkColState.DIE]);
                }
                // 추적 여부
            }

            void Chase ()
            {
                dir = (owner.target.transform.position - owner.transform.position).normalized;
                owner.transform.position = new Vector2 (owner.transform.position.x + (2f * dir.x) * Time.deltaTime,
                    owner.transform.position.y + (2f * dir.y) * Time.deltaTime);
            }

            bool IsAtkRange () =>
                Vector2.Distance (owner.transform.position, owner.target.transform.position) <
                2f ? true : false;
        }
        class DieState : IState
        {
            private AttackCollision owner;
            public DieState (AttackCollision owner) => this.owner = owner;
            public void OnEnter ()
            {
                owner.AtkColState = EAtkColState.DIE;
                owner.stateControl.Stop ();
            }

            public void OnExit ()
            {
                owner.Destroyed ();
            }

            public void Run () { }
        }
    }
}
