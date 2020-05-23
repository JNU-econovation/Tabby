using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class AnimalControl : MonoBehaviour
    {
        // 이벤트
        public delegate void Event ();
        public delegate void EventAnimal (Animal target);
        public delegate void EventEnemy (Enemy enemy);
        // Animal Event
        public event Event EvTargetAnimalNull;
        public event Event EvBeforeTargetAnimalChange;
        public event EventAnimal EvAfterTargetAnimalChange;
        // Enemy Event
        public event Event EvLockOnEnemyNull;
        public event Event EvBeforeLockOnEnemyChange;
        public event EventEnemy EvAfterLockOnEnemyChange;
        // 현재 지정한 동물
        [SerializeField]
        private Animal targetAnimal;
        public Animal TargetAnimal
        {
            get => targetAnimal;
            set
            {
                if (!value.Equals (targetAnimal))
                {
                    if (targetAnimal != null)
                        EvBeforeTargetAnimalChange?.Invoke ();
                    targetAnimal = value;
                    if (value == null)
                    {
                        EvTargetAnimalNull?.Invoke ();
                    }
                    else
                    {
                        EvAfterTargetAnimalChange?.Invoke (targetAnimal);
                    }
                }
            }
        }
        // 현재 LockOn 된 적
        private Enemy lockOnEnemy;
        public Enemy LockOnEnemy
        {
            get => lockOnEnemy;
            set
            {
                if (!value.Equals (lockOnEnemy))
                {
                    if (lockOnEnemy != null)
                        EvBeforeLockOnEnemyChange?.Invoke ();
                    lockOnEnemy = value;
                    if (value == null)
                    {
                        EvLockOnEnemyNull?.Invoke ();
                    }
                    else
                    {
                        EvAfterLockOnEnemyChange?.Invoke (lockOnEnemy);
                    }
                }
                else
                {
                    // 같을 경우 LockOn 해제
                    lockOnEnemy = null;
                    EvLockOnEnemyNull?.Invoke ();
                }
            }
        }
        // 전체 동물
        [SerializeField]
        private List<Animal> animals;
        public List<Animal> Animals { get => animals; set => animals = value; }

        public AnimalStatData tempData;

        private void Awake ()
        {
            BattleManager._instance.AnimalControl = this;
            for (int i = 0; i < Animals.Count; i++)
            {
                Animals[i].InitStat (Instantiate (tempData) as AnimalStatData);
            }
        }
        void Start ()
        {
            InitEvent ();
        }
        private void InitEvent ()
        {
            BattleManager._instance.InputManager.EvClickAnimal +=
                new InputManager.EventAnimal ((x) => TargetAnimal = x);
        }
        /**
         *   UI 초상화 클릭 시 해당 유닛 선택
         *   @param idx
         */
        public void OnClickPortrait (int idx)
        {
            TargetAnimal = Animals[idx];
        }
        /**
         *   해당 enemy에게 모든 Animal LockOn
         *   @param enemy
         */
        public void CmdLockOn (Enemy enemy)
        {
            LockOnEnemy = enemy;
        }
    }
}
