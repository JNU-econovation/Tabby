using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
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
            yield return null;
        }

        public IEnumerator EndState()
        {
            battleState = BattleDefine.EBattleState.End;
            endEvent?.Invoke();
            yield return null;
        }
    }
}
