using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameData;

namespace Battle
{
    public class BattleManager : MonoBehaviour 
    {
        public delegate void StateEvent();
        // 싱글톤
        public static BattleManager _instance;
        public BattleDefine.EBattleState battleState;
        public AnimalManager animalManager;
        public EnemyManager enemyManager;
        // Ready, Playing, Pause, BattleOver, End

        // Action
        public event StateEvent readyEvent;
        public event StateEvent playingEvent;
        public event StateEvent pauseEvent;
        public event StateEvent battleOverEvent;
        public event StateEvent endEvent;

        // BackGrounds
        public SpriteRenderer backgroundRenderer;
        public List<Sprite> backgrounds;

        // IsWin?
        public bool isWin;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            backgroundRenderer.sprite = backgrounds[DataManager._instance.regionIndex];
            StartCoroutine(ReadyState());
        }

        public IEnumerator ReadyState()
        {
            battleState = BattleDefine.EBattleState.Ready;
            readyEvent?.Invoke();
            yield return new WaitForSeconds(2.0f);
            animalManager.StartAnimals();
            enemyManager.StartAnimals();
            StartCoroutine(PlayingState());
        }

        public IEnumerator PlayingState()
        {
            battleState = BattleDefine.EBattleState.Playing;
            playingEvent?.Invoke();
            yield return null;
        }

        public IEnumerator PauseState()
        {
            battleState = BattleDefine.EBattleState.Pause;
            pauseEvent?.Invoke();
            yield return null;
        }

        public IEnumerator BattleOverState()
        {
            battleState = BattleDefine.EBattleState.BattleOver;
            battleOverEvent?.Invoke();
            DataManager._instance.animalExp = new Tuple<int, int>[3];
            DataManager._instance.farmObjects = -1;

            if (isWin)
            {
                // 경험치, 설정
                for (int i = 0; i < 3; i++)
                {
                    if (DataManager._instance.gogoAnimalIndexes[i] == -1)
                    {
                        DataManager._instance.animalExp[i] = new Tuple<int, int>(-1, -1);
                        continue;
                    }
                    DataManager._instance.animalExp[i] =
                        new Tuple<int, int>(DataManager._instance.gogoAnimalIndexes[i], EnemyManager._instance.enemy.animalData.enemyExp);
                    DataManager._instance.playerData.animalDatas[DataManager._instance.gogoAnimalIndexes[i]].exp += EnemyManager._instance.enemy.animalData.enemyExp;
                    DataManager._instance.SaveData<PlayerData>(DataManager._instance.playerData, "/PlayerData/" + 0 + ".json");
                }
                // 아이템 설정
                float rand = UnityEngine.Random.Range(0f, 1f);
                if (rand < EnemyManager._instance.enemy.animalData.farmObjectPercent)
                {
                    DataManager._instance.farmObjects = EnemyManager._instance.enemy.animalData.farmObjectIndex;
                    DataManager._instance.playerData.farmObjectDatas.Add(new FarmObjectData(EnemyManager._instance.enemy.animalData.farmObjectIndex, 0, 0, new DateTime(), false));
                    DataManager._instance.SaveData<PlayerData>(DataManager._instance.playerData, "/PlayerData/" + 0 + ".json");
                }
            }
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(EndState());
        }

        public IEnumerator EndState()
        {
            battleState = BattleDefine.EBattleState.End;
            endEvent?.Invoke();
            yield return null;
        }
    }
}
