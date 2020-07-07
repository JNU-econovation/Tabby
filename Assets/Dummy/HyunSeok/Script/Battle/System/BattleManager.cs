using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class BattleManager : MonoBehaviour 
    {
        // 싱글톤
        public static BattleManager _instance;
        public BattleDefine.EBattleState battleState;
        public AnimalManager animalManager;
        public EnemyManager enemyManager;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(gameObject);
        }
    }
}
