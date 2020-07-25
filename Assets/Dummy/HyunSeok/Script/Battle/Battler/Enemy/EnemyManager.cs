using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

namespace Battle
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager _instance;
        public AnimalController enemy;
        public Transform enemyPos;

        private int endNumber;
        public int EndNumber
        {
            get => endNumber;
            set
            {
                if (value == 1 && BattleManager._instance.battleState == BattleDefine.EBattleState.Playing)
                {
                    // 전투 끝내기
                    EndAnimals();
                    AnimalManager._instance.EndAnimals();
                    BattleManager._instance.isWin = true;
                    DataManager._instance.isWin = false;
                    StartCoroutine(BattleManager._instance.BattleOverState());
                }
                else
                {
                    endNumber = value;
                }
            }
        }

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(gameObject);
            SpawnEnemyUseData();
        }

        public void StartAnimals()
        {
            enemy.SetForceState(BattleDefine.EBattlerState.Idle);
        }

        public void EndAnimals()
        {
            if (enemy.state != BattleDefine.EBattlerState.Down)
                enemy.SetForceState(BattleDefine.EBattlerState.Ready);
        }

        public void SpawnEnemyUseData()
        {
            int enemyIndex = DataManager._instance.regionIndex;
            GameObject enemyObj = Instantiate(Resources.Load("Battle/Enemy/Prefab_Enemy_" + enemyIndex) as GameObject);
            enemyObj.transform.position = enemyPos.transform.position;
            enemy = enemyObj.transform.GetComponentInChildren<AnimalController>();
            enemy.isEnemy = true;
        }
    }
}
