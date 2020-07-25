using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameData;

namespace Battle
{
    [RequireComponent(typeof(Animator))]
    public class AnimalController : MonoBehaviour
    {
        //------------------------------------------------------------ Data
        // 동물 데이터
        public AnimalGameData animalData;
        // 평타 데이터
        [SerializeField]
        protected List<SkillData> attackDatas;
        // 스킬 데이터
        [SerializeField]
        public List<SkillData> skillDatas;
        // 이펙트 데이터
        [SerializeField]
        protected GameObject effectObj;
        //------------------------------------------------------------ Animator
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
        public float stunTime;
        // 현재 걸린 CC 상태 모음
        //------------------------------------------------------------ Animation state
        // 현재 적 상태
        public bool isEnemy;

        protected virtual void Awake()
        {
            animalData = Instantiate(animalData) as AnimalGameData;
            states = new List<IState>();
            activeSkillQueue = new Queue<SkillData>();
            animator = GetComponent<Animator>();
            InitState();
        }

        protected virtual void Update()
        {
            currentState.Run();
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
            SetState(BattleDefine.EBattlerState.Ready);
        }

        public virtual void Damaged(SkillData skillData, float focus, Tuple<float, bool> damage)
        {
            if (animalData.HP < 1)
                return;

            if (skillData == null)
                return;
            // 명중률
            float enemyFocusRandom = UnityEngine.Random.Range(0.0f, 1.0f);
            if (damage.Item1 > 2)
            {
                GameObject dmgObj = Instantiate(AnimalManager._instance.damagePrefab);
                dmgObj.SetActive(true);
                dmgObj.transform.position = transform.position + new Vector3(0f, 1f, 0f);
                // Miss!!!
                if (enemyFocusRandom > focus)
                {
                    dmgObj.GetComponent<UIDamage>().SetDmg(damage.Item1, damage.Item2, true);
                    return;
                }
                dmgObj.GetComponent<UIDamage>().SetDmg(damage.Item1, damage.Item2, false);
            }
            // 만약 죽을 경우
            if (animalData.HP - damage.Item1 < 1)
            {
                animalData.HP = 0;
                SetForceState(BattleDefine.EBattlerState.Down);
                return;
            }
            // 데미지 적용
            animalData.HP -= damage.Item1;
            Coroutine ccCrtn = null;
            // cc Time 조정
            float toughCCTime = skillData.ccTime * (1 - animalData.Tough);
            // CC 적용
            switch (skillData.ccType)
            {
                case BattleDefine.ESkillCCType.Stun:
                    SetForceState(BattleDefine.EBattlerState.Stun);
                    ccCrtn = StartCoroutine(CCStun(skillData, toughCCTime));
                    break;
                case BattleDefine.ESkillCCType.StatDown_int:
                    ccCrtn = StartCoroutine(CCStatDownInt(skillData, toughCCTime));
                    break;
                case BattleDefine.ESkillCCType.StatDown_Percent:
                    ccCrtn = StartCoroutine(CCStatDownPercent(skillData, toughCCTime));
                    break;

            }
        }

        protected virtual IEnumerator CCStun(SkillData data, float ccTime)
        {
            stunTime += ccTime;
            float time = 0f;
            while (time < ccTime)
            {
                time += Time.deltaTime;
                stunTime -= Time.deltaTime;
                yield return null;
            }
            if (stunTime < 0.05f)
            {
                SetForceState(BattleDefine.EBattlerState.Idle);
            }
        }

        protected virtual IEnumerator CCStatDownInt(SkillData data, float ccTime)
        {
            switch(data.statType)
            {
                case BattleDefine.ESkillStatType.Atk:
                    animalData.Atk -= data.ccPower;
                    break;
                case BattleDefine.ESkillStatType.AtkSpd:
                    animalData.AtkSpd -= data.ccPower;
                    break;
                case BattleDefine.ESkillStatType.Critical:
                    animalData.Critical -= data.ccPower;
                    break;
                case BattleDefine.ESkillStatType.Focus:
                    animalData.Focus -= data.ccPower;
                    break;
                case BattleDefine.ESkillStatType.Tough:
                    animalData.Tough -= data.ccPower;
                    break;
                default:
                    break;
            }
            float time = 0f;
            while (time < ccTime)
            {
                time += Time.deltaTime;
                yield return null;
            }
            switch (data.statType)
            {
                case BattleDefine.ESkillStatType.Atk:
                    animalData.Atk += data.ccPower;
                    break;
                case BattleDefine.ESkillStatType.AtkSpd:
                    animalData.AtkSpd += data.ccPower;
                    break;
                case BattleDefine.ESkillStatType.Critical:
                    animalData.Critical += data.ccPower;
                    break;
                case BattleDefine.ESkillStatType.Focus:
                    animalData.Focus += data.ccPower;
                    break;
                case BattleDefine.ESkillStatType.Tough:
                    animalData.Tough += data.ccPower;
                    break;
                default:
                    break;
            }
        }

        protected virtual IEnumerator CCStatDownPercent(SkillData data, float ccTime)
        {
            switch (data.statType)
            {
                case BattleDefine.ESkillStatType.Atk:
                    animalData.Atk *= data.ccPower;
                    break;
                case BattleDefine.ESkillStatType.AtkSpd:
                    animalData.AtkSpd *= data.ccPower;
                    break;
                case BattleDefine.ESkillStatType.Critical:
                    animalData.Critical *= data.ccPower;
                    break;
                case BattleDefine.ESkillStatType.Focus:
                    animalData.Focus *= data.ccPower;
                    break;
                case BattleDefine.ESkillStatType.Tough:
                    animalData.Tough *= data.ccPower;
                    break;
                default:
                    break;
            }
            float time = 0f;
            while (time < ccTime)
            {
                time += Time.deltaTime;
                yield return null;
            }
            switch (data.statType)
            {
                case BattleDefine.ESkillStatType.Atk:
                    animalData.Atk /= data.ccPower;
                    break;
                case BattleDefine.ESkillStatType.AtkSpd:
                    animalData.AtkSpd /= data.ccPower;
                    break;
                case BattleDefine.ESkillStatType.Critical:
                    animalData.Critical /= data.ccPower;
                    break;
                case BattleDefine.ESkillStatType.Focus:
                    animalData.Focus /= data.ccPower;
                    break;
                case BattleDefine.ESkillStatType.Tough:
                    animalData.Tough /= data.ccPower;
                    break;
                default:
                    break;
            }
        }

        public virtual void SetState(BattleDefine.EBattlerState state)
        {
            /*if (!currentState.IsDeny(state))
                return;*/
            if (this.state == BattleDefine.EBattlerState.Down)
                return;
            if (this.state == BattleDefine.EBattlerState.Stun)
                return;
            if (this.state == BattleDefine.EBattlerState.Ready)
                return;
            if (currentState != null)
                currentState.OnExit();
            currentState = states[(int)state];
            currentState.OnEnter();
        }
        public virtual void SetForceState(BattleDefine.EBattlerState state)
        {
            if (this.state == BattleDefine.EBattlerState.Down)
                return;
            if (currentState != null)
                currentState.OnExit();
            currentState = states[(int)state];
            currentState.OnEnter();
        }
        // 스킬 발동 시
        public virtual bool OnClickSkill()
        {
            if (stunTime < 0.08f)
            {
                activeSkillQueue.Clear();
                foreach (SkillData data in skillDatas)
                    activeSkillQueue.Enqueue(data);
                SetState(BattleDefine.EBattlerState.Skill);
                return true;
            }
            else
            {
                return false;
            }
        }

        // 스킬 파워 계산 식
        public Tuple<float, bool> CaculateDamage(SkillData data)
        {
            if (data == null)
                return new Tuple<float, bool>(0f, false);
            bool isCritical = false;
            float dmg = (float)data.skillPowerInt + animalData.Atk * (data.skillPowerPercent);
            dmg += UnityEngine.Random.Range(-5f, 5f);
            float criticalRandom = UnityEngine.Random.Range(0.0f, 1.0f);
            // 크리 발동
            if (criticalRandom < animalData.Critical)
            {
                dmg *= 2;
                isCritical = true;
            }
            return new Tuple<float, bool>(Mathf.Round(dmg), isCritical);
        }
        // 공격 모션 시전 시
        public virtual void ActiveSkill()
        {
            if (state == BattleDefine.EBattlerState.Down)
                return;
            List<AnimalController> atkTargets = new List<AnimalController>();
            // Damage!
            if (currentSkillData == null)
                return;
            switch (currentSkillData.target)
            {
                case BattleDefine.ESkillTarget.Enemy:
                    atkTargets.Add(EnemyManager._instance.enemy);
                    break;
                case BattleDefine.ESkillTarget.Me:
                    atkTargets.Add(this);
                    break;
                case BattleDefine.ESkillTarget.TeamAll:
                    for (int i = 0; i < 3; i++)
                    {
                        atkTargets.Add(AnimalManager._instance.animals[i]);
                    }
                    break;
                case BattleDefine.ESkillTarget.TeamExceptMe:
                    for (int i = 0; i < 3; i++)
                    {
                        if (i == animalData.BattleIndex)
                        {
                            continue;
                        }
                        atkTargets.Add(AnimalManager._instance.animals[i]);
                    }
                    break;
                case BattleDefine.ESkillTarget.TeamBackAll:
                    for (int i = 0; i < 3; i++)
                    {
                        if (i <= animalData.BattleIndex)
                        {
                            continue;
                        }
                        atkTargets.Add(AnimalManager._instance.animals[i]);
                    }
                    break;
                case BattleDefine.ESkillTarget.TeamBackOne:
                    for (int i = 0; i < 3; i++)
                    {
                        if (i == animalData.BattleIndex + 1)
                        {
                            atkTargets.Add(AnimalManager._instance.animals[i]);
                        }
                    }
                    break;
                case BattleDefine.ESkillTarget.TeamFrontAll:
                    for (int i = 0; i < 3; i++)
                    {
                        if (i >= animalData.BattleIndex)
                        {
                            continue;
                        }
                        atkTargets.Add(AnimalManager._instance.animals[i]);
                    }
                    break;
                case BattleDefine.ESkillTarget.TeamFrontOne:
                    for (int i = 0; i < 3; i++)
                    {
                        if (i == animalData.BattleIndex - 1)
                        {
                            continue;
                        }
                        atkTargets.Add(AnimalManager._instance.animals[i]);
                    }
                    break;
                case BattleDefine.ESkillTarget.FrontAll:
                    atkTargets.Add(EnemyManager._instance.enemy);
                    for (int i = 0; i < 3; i++)
                    {
                        if (i >= animalData.BattleIndex)
                        {
                            continue;
                        }
                        atkTargets.Add(AnimalManager._instance.animals[i]);
                    }
                    break;
                case BattleDefine.ESkillTarget.One:
                    atkTargets.Add(AnimalManager._instance.animals[0]);
                    break;
                case BattleDefine.ESkillTarget.Two:
                    atkTargets.Add(AnimalManager._instance.animals[1]);
                    break;
                case BattleDefine.ESkillTarget.Three:
                    atkTargets.Add(AnimalManager._instance.animals[2]);
                    break;
                case BattleDefine.ESkillTarget.All:
                    atkTargets.Add(EnemyManager._instance.enemy);
                    for (int i = 0; i < 3; i++)
                    {
                        atkTargets.Add(AnimalManager._instance.animals[i]);
                    }
                    break;
                case BattleDefine.ESkillTarget.TeamRandom:
                    List<int> posAnimalIndex = new List<int>();
                    for (int i = 0; i< 3; i++)
                    {
                        if (AnimalManager._instance.animals[i] != null && AnimalManager._instance.animals[i].animalData.HP > 1)
                            posAnimalIndex.Add(i);
                    }
                    int rand = UnityEngine.Random.Range(0, posAnimalIndex.Count);
                    atkTargets.Add(AnimalManager._instance.animals[posAnimalIndex[rand]]);
                    break;
                case BattleDefine.ESkillTarget.TeamRandomTwo:
                    for (int i = 0; i < 3; i++)
                    {
                        atkTargets.Add(AnimalManager._instance.animals[i]);
                    }
                    List<int> posAnimalIndex2 = new List<int>();
                    for (int i = 0; i < 3; i++)
                    {
                        if (AnimalManager._instance.animals[i] != null && AnimalManager._instance.animals[i].animalData.HP > 1)
                            posAnimalIndex2.Add(i);
                    }
                    if (posAnimalIndex2.Count > 2)
                    {
                        int rand2 = UnityEngine.Random.Range(0, posAnimalIndex2.Count);
                        atkTargets.Remove(AnimalManager._instance.animals[posAnimalIndex2[rand2]]);
                    }
                    break;
                default:
                    atkTargets.Add(EnemyManager._instance.enemy);
                    break;
            }
            foreach(AnimalController animal in atkTargets)
            {
                if (animal != null)
                    animal.Damaged(currentSkillData, animalData.Focus, CaculateDamage(currentSkillData));
            }

        }
        // 스킬 종료 시
        public virtual void EndSkill()
        {
            if (state == BattleDefine.EBattlerState.Down)
                return;
            if (activeSkillQueue.Count > 0)
            {
                SetState(BattleDefine.EBattlerState.Skill);
            }
            else
            {
                SetState(BattleDefine.EBattlerState.Idle);
            }
        }
        // 이펙트 발사
        public virtual void OnEffect()
        {
            if (effectObj != null)
                effectObj.SetActive(true);
        }

        // 카메라 진동
        public virtual void ShakeCamera()
        {
            StartCoroutine(CameraShake._instance.ShakeCamera(0.65f, animalData.enemyShake, 1f));
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
                maxAttackDelay = 1 / (owner.animalData.AtkSpd + UnityEngine.Random.Range(-0.1f, 0.1f));
                owner.state = BattleDefine.EBattlerState.Idle;
                //owner.animator.SetTrigger("TrgIdle");
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
                    if (owner.attackDatas.Count > 1)
                    {
                        for (int i = 0; i < owner.attackDatas.Count; i++)
                        {
                            if (i == owner.attackDatas.Count - 1)
                            {
                                owner.activeSkillQueue.Enqueue(owner.attackDatas[i]);
                            }
                            else
                            {
                                float rand = UnityEngine.Random.Range(0f, 1f);
                                if (rand < owner.attackDatas[i].patternPercent)
                                {
                                    owner.activeSkillQueue.Enqueue(owner.attackDatas[i]);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        owner.activeSkillQueue.Enqueue(owner.attackDatas[0]);
                    }
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
                owner.animator.SetTrigger("TrgStun");
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
                owner.animator.SetTrigger("TrgStun");
                owner.currentSkillData = null;
                if (!owner.isEnemy)
                    AnimalManager._instance.EndNumber++;
                else
                    EnemyManager._instance.EndNumber++;
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

