using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

namespace Battle
{
    public class AnimalController : MonoBehaviour, IBattlerAdapter
    {
        //------------------------------------------------------------ Data
        // 동물 데이터
        public AnimalGameData animalData;
        // 평타 데이터
        [SerializeField]
        private SkillData attackData;
        // 스킬 데이터
        [SerializeField]
        protected SkillData skillData;
        //------------------------------------------------------------ 
        // 현재 발동 대기 중인 스킬들
        [SerializeField]
        protected Queue<SkillData> activeSkillQueue;
        // 현재 발동중인 스킬
        [SerializeField]
        protected SkillData currentSkillData;
        //------------------------------------------------------------ Data
        // 현재 동작하고 있는 상태
        public BattleDefine.EBattlerState state;
        // 현재 동작하고 있는 상태 클래스
        public IState currentState;
        // 상태 모음
        public List<IState> states;
        // 현재 걸린 CC 모음
        public List<Coroutine> CCs;
        // 현재 걸린 CC 상태 모음
        public List<BattleDefine.ECCState> CCStates;

        public void InitState()
        {

        }

        public void Damaged(SkillData skillData)
        {
            throw new System.NotImplementedException();
        }

        public void SetState(BattleDefine.EBattlerState state)
        {
            if (!currentState.IsDeny(state))
                return;
            currentState.OnExit();
            currentState = states[(int)state];
            currentState.OnEnter();
        }
        // 공격 모션 시전 시
        public void ActiveSkill()
        {

        }
        // 스킬 종료 시
        public void EndSkill()
        {
            SetState(BattleDefine.EBattlerState.Idle);
        }

        class StateIdle : IState
        {
            AnimalController owner;
            float attackDelay;

            public StateIdle(AnimalController argOwner)
            {
                owner = argOwner;
            }

            public bool IsDeny(BattleDefine.EBattlerState state)
            {
                throw new System.NotImplementedException();
            }

            public void OnEnter()
            {
                owner.state = BattleDefine.EBattlerState.Idle;
                attackDelay = 0;
            }

            public void OnExit()
            {

            }

            public void Run()
            {
                // 만약 남아있는 스킬이 있을 경우
                if (owner.activeSkillQueue.Count != 0)
                {
                    owner.activeSkillQueue.Enqueue(owner.skillData);
                    owner.SetState(BattleDefine.EBattlerState.Skill);
                }
                // 평타
                attackDelay += Time.deltaTime;
                if (attackDelay > owner.animalData.AtkSpd)
                {
                    owner.activeSkillQueue.Enqueue(owner.attackData);
                    owner.SetState(BattleDefine.EBattlerState.Skill);
                }
            }
        }   
        class StateSkill : IState
        {
            AnimalController owner;
            float castingTime;
            bool isCasting;

            public StateSkill(AnimalController argOwner)
            {
                owner = argOwner;
            }

            public bool IsDeny(BattleDefine.EBattlerState state)
            {
                // 만약 현재 스킬이 캐스팅 중 취소될 수 있으며 현재 캐스팅 중이라면
                if (owner.currentSkillData.CastingType == BattleDefine.ESkillCastingType.Cancled
                    && isCasting)
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
                castingTime = 0;
                isCasting = true;
                owner.currentSkillData = owner.activeSkillQueue.Dequeue();
            }

            public void OnExit()
            {
                owner.currentSkillData = null;
            }

            public void Run()
            {
                if (isCasting)
                {
                    castingTime += Time.deltaTime;
                    if (castingTime > owner.currentSkillData.CastingTime)
                    {
                        // 애니메이션 변경
                        isCasting = false;
                    }
                }
            }
        }
    }
}

