using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

namespace Battle
{
    public class RedTurtle : Animal
    {
        void Awake ()
        {
            InitFSM ();
        }
        void Update ()
        {
            if (Input.GetKeyDown (KeyCode.W))
            {
                stateControl.SetState (states[(int) EAnimalState.MOVE]);
            }
            stateControl.Run ();
        }
        public override void InitStat (AnimalStatData data)
        {
            stat = new AnimalBattleData (data);
        }
        protected override void InitFSM ()
        {
            // 기본 상태
            AnimalState = EAnimalState.DETECT_AUTO;
            // State 생성
            states[(int) EAnimalState.IDLE] = new IdleState (this);
            states[(int) EAnimalState.MOVE] = new MoveState (this);
            states[(int) EAnimalState.DETECT_AUTO] = new DetectAutoState (this);
            states[(int) EAnimalState.ATK] = new AtkState (this);
            // HeadMachine 생성
            stateControl = new HeadMachine<Animal> (states[(int) AnimalState]);
        }

        class IdleState : IState
        {
            private RedTurtle owner;
            public IdleState (RedTurtle owner) => this.owner = owner;
            public void OnEnter ()
            {
                owner.AnimalState = EAnimalState.IDLE;
            }

            public void OnExit ()
            {
            }

            public void Run ()
            {
            }
        }
        class MoveState : IState
        {
            float time;
            private RedTurtle owner;
            public MoveState (RedTurtle owner) => this.owner = owner;

            public void OnEnter ()
            {
                time = 0f;
                owner.AnimalState = EAnimalState.MOVE;
            }

            public void OnExit ()
            {
            }

            public void Run ()
            {
                time += Time.deltaTime;
                if (time > 2.0f)
                {
                    owner.stateControl.Revert ();
                }

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
                owner.AnimalState = EAnimalState.DETECT_AUTO;
                owner.target = BattleManager._instance.Enemies[Detect ()];
                targetDistance = 999999f;
                detectTime = 0f;
                detectDelay = 0.1f;

                if (IsAtkRange ())
                {
                    owner.stateControl.SetState (owner.states[(int) EAnimalState.ATK]);
                }
            }

            public void OnExit ()
            {
                owner.target = BattleManager._instance.Enemies[Detect ()];
            }

            public void Run ()
            {
                detectTime += Time.deltaTime;
                if (detectTime > detectDelay)
                {
                    owner.target = BattleManager._instance.Enemies[Detect ()];
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
                for (int i = 0; i < BattleManager._instance.Enemies.Count; i++)
                {
                    float distance = Vector2.SqrMagnitude (BattleManager._instance.Enemies[i].transform.position - owner.transform.position);
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
            bool IsAtkRange () => Vector2.SqrMagnitude (owner.target.transform.position - owner.transform.position) < 2f ? true : false;
        }
        class AtkState : IState
        {
            float time;
            private RedTurtle owner;
            public AtkState (RedTurtle owner) => this.owner = owner;

            public void OnEnter ()
            {
                owner.AnimalState = EAnimalState.DETECT_AUTO;
                time = 0f;
            }

            public void OnExit ()
            {
            }

            public void Run ()
            {
                time += Time.deltaTime;
                if (time > 2.0f)
                {
                    owner.stateControl.Revert ();
                }
            }
        }
    }
}
