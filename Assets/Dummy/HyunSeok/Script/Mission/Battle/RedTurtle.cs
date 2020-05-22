using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

namespace Battle
{
    public class RedTurtle : Animal
    {
        #region Command state
        MoveState moveState;
        DetectLockOnState detectLockOnState;
        #endregion
        void Awake ()
        {
            // Init Reference
            InitRef ();
            // Init FSM setting
            InitFSM ();
            // Init Command state
            moveState = states[(int) EAnimalState.MOVE] as MoveState;
            detectLockOnState = states[(int) EAnimalState.DETECT_LOCKON] as DetectLockOnState;
        }
        void Start ()
        {
            // Init Event
            InitEvent ();
        }
        void Update ()
        {
            stateControl.Run ();
        }
        public override void InitStat (AnimalStatData data)
        {
            stat = new AnimalBattleData (data);
        }
        private void InitRef ()
        {
            battleVisual = GetComponentInChildren<BattleVisual> ();
        }
        private void InitEvent ()
        {
            BattleManager._instance.AnimalControl.EvAfterLockOnEnemyChange +=
                new AnimalControl.EventEnemy (CmdLockOn);
        }
        protected override void InitFSM ()
        {
            // 기본 상태
            AnimalState = EAnimalState.IDLE;
            // State 생성
            states[(int) EAnimalState.IDLE] = new IdleState (this);
            states[(int) EAnimalState.MOVE] = new MoveState (this);
            states[(int) EAnimalState.DETECT_AUTO] = new DetectAutoState (this);
            states[(int) EAnimalState.DETECT_LOCKON] = new DetectLockOnState (this);
            states[(int) EAnimalState.ATK] = new AtkState (this);
            // HeadMachine 생성
            stateControl = new HeadMachine<Animal> (states[(int) AnimalState]);
        }

        public override void CmdMove (Vector3 dir, float dist)
        {
            moveState.InitPos (dir, dist);
            stateControl.SetState (states[(int) EAnimalState.MOVE]);
        }

        public override void CmdLockOn (Enemy enemy)
        {
            stateControl.SetState (states[(int) EAnimalState.IDLE]);
        }

        class IdleState : IState
        {
            private RedTurtle owner;
            public IdleState (RedTurtle owner) => this.owner = owner;
            public void OnEnter ()
            {
                owner.AnimalState = EAnimalState.IDLE;
            }

            public void OnExit () { }

            public void Run ()
            {
                // 만약 적이 있을 경우
                if (BattleManager._instance.EnemyControl.Enemies.Count > 0)
                {
                    // 락온 상태 여부 파악
                    if (BattleManager._instance.AnimalControl.LockOnEnemy != null)
                    {
                        owner.stateControl.SetState (owner.states[(int) EAnimalState.DETECT_LOCKON]);
                    }
                    else
                    {
                        owner.stateControl.SetState (owner.states[(int) EAnimalState.DETECT_AUTO]);
                    }
                }
            }
        }
        class MoveState : IState
        {
            public Vector2 direction;
            public float maxDist;
            float currentDist;
            Vector2 beginPos;

            private RedTurtle owner;
            public MoveState (RedTurtle owner) => this.owner = owner;

            public void OnEnter ()
            {
                owner.AnimalState = EAnimalState.MOVE;
            }

            public void OnExit ()
            {

            }

            public void Run ()
            {
                currentDist += 10f * Time.deltaTime;
                owner.transform.position = new Vector2 (beginPos.x + (currentDist * direction.x),
                    beginPos.y + (currentDist * direction.y));
                if (currentDist > maxDist)
                {
                    if (owner.isLockOn)
                        owner.stateControl.SetState (owner.states[(int) EAnimalState.DETECT_LOCKON]);
                    else
                        owner.stateControl.SetState (owner.states[(int) EAnimalState.DETECT_AUTO]);
                }

            }
            public void InitPos (Vector3 dir, float dist)
            {
                direction = dir;
                maxDist = dist;
                currentDist = 0f;
                beginPos = owner.transform.position;
            }
        }
        class DetectAutoState : IState
        {
            int targetIdx;
            float targetDistance;
            // 탐색 연산량 조절
            float detectTime;
            float detectDelay;
            private RedTurtle owner;
            public DetectAutoState (RedTurtle owner) => this.owner = owner;

            public void OnEnter ()
            {
                // 적이 없을 경우 대기 상태
                if (BattleManager._instance.EnemyControl.Enemies.Count < 1)
                    owner.stateControl.SetState (owner.states[(int) EAnimalState.IDLE]);
                owner.AnimalState = EAnimalState.DETECT_AUTO;
                owner.target = BattleManager._instance.EnemyControl.Enemies[Detect ()];
                targetDistance = 999999f;
                detectTime = 0f;
                detectDelay = 0.1f;

                if (IsAtkRange ())
                {
                    owner.stateControl.SetState (owner.states[(int) EAnimalState.ATK]);
                }
            }

            public void OnExit () { }

            public void Run ()
            {
                // 적이 없을 경우 대기 상태
                if (BattleManager._instance.EnemyControl.Enemies.Count < 1)
                    owner.stateControl.SetState (owner.states[(int) EAnimalState.IDLE]);
                detectTime += Time.deltaTime;
                if (detectTime > detectDelay)
                {
                    owner.target = BattleManager._instance.EnemyControl.Enemies[Detect ()];
                    targetDistance = 999999f;
                    detectTime = 0f;
                    // 만약 공격 범위 안에 들어온 경우
                    if (IsAtkRange ())
                    {
                        owner.stateControl.SetState (owner.states[(int) EAnimalState.ATK]);
                    }
                }
                // 항상 적 추적
                Chase ();
            }
            // Enemy 모두 탐색하여 거리 측정 후 가장 가까운 적 도출
            int Detect ()
            {
                int detectIdx = 0;
                for (int i = 0; i < BattleManager._instance.EnemyControl.Enemies.Count; i++)
                {
                    float distance = Vector2.SqrMagnitude (BattleManager._instance.EnemyControl.Enemies[i].transform.position - owner.transform.position);
                    if (distance < targetDistance)
                    {
                        targetDistance = distance;
                        detectIdx = i;
                    }
                }
                return detectIdx;
            }
            // 적 추적
            void Chase ()
            {
                Vector2 direction = (owner.target.transform.position - owner.transform.position).normalized;
                owner.transform.position = new Vector2 (owner.transform.position.x + (2f * direction.x) * Time.deltaTime,
                    owner.transform.position.y + (2f * direction.y) * Time.deltaTime);
            }
            // 공격 범위에 적 들어왔는지 확인
            bool IsAtkRange () =>
                Vector2.Distance (owner.transform.position, owner.target.transform.position) <
                owner.stat.AtkRange ? true : false;
        }
        class DetectLockOnState : IState
        {
            // 탐색 연산량 조절
            float detectTime;
            float detectDelay;
            private RedTurtle owner;
            public DetectLockOnState (RedTurtle owner) => this.owner = owner;

            public void OnEnter ()
            {
                // 적이 없을 경우 대기 상태
                if (BattleManager._instance.AnimalControl.LockOnEnemy == null)
                    owner.stateControl.SetState (owner.states[(int) EAnimalState.IDLE]);
                owner.AnimalState = EAnimalState.DETECT_LOCKON;
                owner.target = BattleManager._instance.AnimalControl.LockOnEnemy;
                detectTime = 0f;
                detectDelay = 0.1f;

                if (IsAtkRange ())
                {
                    owner.stateControl.SetState (owner.states[(int) EAnimalState.ATK]);
                }
            }

            public void OnExit () { }

            public void Run ()
            {
                if (BattleManager._instance.AnimalControl.LockOnEnemy == null)
                    owner.stateControl.SetState (owner.states[(int) EAnimalState.IDLE]);
                detectTime += Time.deltaTime;
                if (detectTime > detectDelay)
                {
                    detectTime = 0f;
                    // 만약 공격 범위 안에 들어온 경우
                    if (IsAtkRange ())
                    {
                        owner.stateControl.SetState (owner.states[(int) EAnimalState.ATK]);
                    }
                }
                // 항상 적 추적
                Chase ();
            }
            // 적 추적
            void Chase ()
            {
                Vector2 direction = (owner.target.transform.position - owner.transform.position).normalized;
                owner.transform.position = new Vector2 (owner.transform.position.x + (2f * direction.x) * Time.deltaTime,
                    owner.transform.position.y + (2f * direction.y) * Time.deltaTime);
            }
            // 공격 범위에 적 들어왔는지 확인
            bool IsAtkRange () =>
                Vector2.Distance (owner.transform.position, owner.target.transform.position) <
                owner.stat.AtkRange ? true : false;
        }
        class AtkState : IState
        {
            float time;
            private RedTurtle owner;
            public AtkState (RedTurtle owner) => this.owner = owner;

            public void OnEnter ()
            {
                owner.AnimalState = EAnimalState.ATK;
                time = 0f;
            }

            public void OnExit () { }

            public void Run ()
            {
                time += Time.deltaTime;
                if (time > owner.stat.AtkSpd)
                {
                    owner.stateControl.SetState (owner.states[(int) EAnimalState.IDLE]);
                }
            }
        }
    }
}
