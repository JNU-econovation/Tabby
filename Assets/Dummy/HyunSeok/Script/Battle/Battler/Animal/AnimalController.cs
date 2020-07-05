using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

namespace Battle
{
    [RequireComponent(typeof(Animator))]
    public class AnimalController : MonoBehaviour, IBattlerAdapter
    {
        //------------------------------------------------------------ Data
        // 동물 데이터
        public AnimalGameData animalData;
        // 평타 데이터
        [SerializeField]
        protected SkillData attackData;
        // 스킬 데이터
        [SerializeField]
        protected List<SkillData> skillDatas;
        //------------------------------------------------------------ Animator
        [SerializeField]
        protected Animator animator;
        //------------------------------------------------------------ Skill system
        // 현재 발동 대기 중인 스킬들
        protected Queue<SkillData> activeSkillQueue;
        // 현재 발동중인 스킬
        protected SkillData currentSkillData;
        //------------------------------------------------------------ Data
        // 현재 동작하고 있는 상태
        public BattleDefine.EBattlerState state;
        // 현재 동작하고 있는 상태 클래스
        protected IState currentState;
        // 상태 모음
        public List<IState> states;
        // 현재 걸린 CC 모음
        public List<Coroutine> crowdControls;
        // 현재 걸린 CC 상태 모음
        //------------------------------------------------------------ Animation state

        protected virtual void Awake()
        {
            states = new List<IState>();
            activeSkillQueue = new Queue<SkillData>();
            crowdControls = new List<Coroutine>();
            animator = GetComponent<Animator>();
            InitState();
        }

        protected virtual void Update()
        {
            currentState.Run();

            if (Input.GetKeyUp(KeyCode.A))
            {
                OnClickSkill();
            }
        }

        protected virtual void InitState()
        {
            states.Add(new StateReady(this));
            states.Add(new StateIdle(this));
            states.Add(new StateSkill(this));
            states.Add(new StateStun(this));
            states.Add(new StateDamaged(this));
            states.Add(new StateDown(this));
            currentState = states[(int)state];
            SetState(BattleDefine.EBattlerState.Idle);
        }

        public virtual void Damaged(SkillData skillData, float damage)
        {
            // 데미지 적용
            animalData.HP -= damage;
            // CC 적용
            
        }

        protected virtual void DeleteCC(CrowdControl cc)
        {
            //crowdControls.Remove(cc);
        }

        public virtual void SetState(BattleDefine.EBattlerState state)
        {
            if (!currentState.IsDeny(state))
                return;
            if (currentState != null)
                currentState.OnExit();
            currentState = states[(int)state];
            currentState.OnEnter();
        }
        public virtual void SetForceState(BattleDefine.EBattlerState state)
        {
            if (currentState != null)
                currentState.OnExit();
            currentState = states[(int)state];
            currentState.OnEnter();
        }
        // 스킬 발동 시
        public virtual void OnClickSkill()
        {
            activeSkillQueue.Clear();
            foreach(SkillData data in skillDatas)
                activeSkillQueue.Enqueue(data);
            SetState(BattleDefine.EBattlerState.Skill);
        }
        // 공격 모션 시전 시
        public void ActiveSkill()
        {
            // Damage!
        }
        // 스킬 종료 시
        public void EndSkill()
        {
            if (activeSkillQueue.Count > 0)
            {
                SetState(BattleDefine.EBattlerState.Skill);
            }
            else
            {
                SetForceState(BattleDefine.EBattlerState.Idle);
            }
        }
        //---------------------------------------------------------------- CC Class
        public class CrowdControl
        {
            AnimalController owner;
            SkillData ccData;
            float ccTime;
            public CrowdControl(AnimalController animalControl, SkillData skillData)
            {
                owner = animalControl;
                skillData = ccData;
            }
            void Start()
            {

            }
            void Run()
            {
                // CC 끝나면 종료하기
                ccTime += Time.deltaTime;
                if (ccTime > ccData.ccTime)
                {
                    End();
                }
                // CC 적용하기
            }
            void End()
            {
                owner.DeleteCC(this);
            }
        }
        //---------------------------------------------------------------- State Class
        class StateReady : IState
        {
            AnimalController owner;

            public StateReady(AnimalController argOwner)
            {
                owner = argOwner;
            }

            public bool IsDeny(BattleDefine.EBattlerState state)
            {
                return true;
            }

            public void OnEnter()
            {
                owner.state = BattleDefine.EBattlerState.Ready;
            }

            public void OnExit()
            {

            }

            public void Run()
            {
               
            }
        }
        class StateIdle : IState
        {
            AnimalController owner;
            float attackDelay;
            float maxAttackDelay;

            public StateIdle(AnimalController argOwner)
            {
                owner = argOwner;

            }

            public bool IsDeny(BattleDefine.EBattlerState state)
            {
                return true;
            }

            public void OnEnter()
            {
                attackDelay = 0;
                maxAttackDelay = owner.animalData.AtkSpd + Random.Range(-0.2f, 0.2f);
                owner.state = BattleDefine.EBattlerState.Idle;
                owner.animator.SetTrigger("TrgIdle");
            }

            public void OnExit()
            {

            }

            public void Run()
            {
                // 평타
                attackDelay += Time.deltaTime;
                if (attackDelay > maxAttackDelay)
                {
                    owner.activeSkillQueue.Clear();
                    owner.activeSkillQueue.Enqueue(owner.attackData);
                    owner.SetState(BattleDefine.EBattlerState.Skill);
                }
            }
        }   
        class StateSkill : IState
        {
            AnimalController owner;
            bool isEnd;

            public StateSkill(AnimalController argOwner)
            {
                owner = argOwner;
            }

            public bool IsDeny(BattleDefine.EBattlerState state)
            {
                // 만약 현재 스킬이 캐스팅 중 취소될 수 있으면 방해가능
                if (owner.currentSkillData.castingType == BattleDefine.ESkillCastingType.Cancled)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void OnEnter()
            {
                owner.state = BattleDefine.EBattlerState.Skill;
                owner.currentSkillData = owner.activeSkillQueue.Dequeue();

                if (owner.currentSkillData.type == BattleDefine.ESkillType.Attack)
                    owner.animator.SetTrigger("TrgAtk");
                else
                    owner.animator.SetTrigger("TrgSkill" + owner.currentSkillData.index);
            }

            public void OnExit()
            {
                owner.currentSkillData = null;
            }

            public void Run()
            {

            }
        }
        class StateDamaged : IState
        {
            AnimalController owner;

            public StateDamaged(AnimalController argOwner)
            {
                owner = argOwner;
            }

            public bool IsDeny(BattleDefine.EBattlerState state)
            {
                throw new System.NotImplementedException();
            }

            public void OnEnter()
            {
                owner.state = BattleDefine.EBattlerState.Damaged;
            }

            public void OnExit()
            {

            }

            public void Run()
            {
                
            }
        }
        class StateStun : IState
        {
            AnimalController owner;

            public StateStun(AnimalController argOwner)
            {
                owner = argOwner;
            }

            public bool IsDeny(BattleDefine.EBattlerState state)
            {
                return false;
            }

            public void OnEnter()
            {
                owner.state = BattleDefine.EBattlerState.Stun;
            }

            public void OnExit()
            {

            }

            public void Run()
            {

            }
        }
        class StateDown : IState
        {
            AnimalController owner;

            public StateDown(AnimalController argOwner)
            {
                owner = argOwner;
            }

            public bool IsDeny(BattleDefine.EBattlerState state)
            {
                return false;
            }

            public void OnEnter()
            {
                owner.state = BattleDefine.EBattlerState.Down;
            }

            public void OnExit()
            {

            }

            public void Run()
            {

            }
        }
    }
}

