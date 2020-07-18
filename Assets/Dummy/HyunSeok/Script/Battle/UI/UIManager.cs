using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class UIManager : MonoBehaviour
    {
        public List<GameObject> panels;
        public static UIManager _instance;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            BattleManager._instance.pauseEvent += OnPausePanel;
            BattleManager._instance.battleOverEvent += OnBattleOverPanel;
            BattleManager._instance.endEvent += OnEndPanel;
            StartCoroutine(OnReadyPanel());
        }


        private IEnumerator OnReadyPanel()
        {
            OnPanel(0);
            yield return new WaitForSeconds(2.0f);
            OnPlayingPanel();
        }

        public void OnPlayingPanel()
        {
            OnPanel(1);
        }

        public void OnPausePanel()
        {
            Time.timeScale = 0f;
            OnPanel(2);
        }

        private void OnBattleOverPanel()
        {
            OnPanel(3);
        }

        private void OnEndPanel()
        {
            OnPanel(4);
        }

        public void OnPanel(int index)
        {
            for(int i = 0; i < panels.Count; i++)
            {
                if (i != index)
                {
                    panels[i].SetActive(false);
                }
                else
                {
                    panels[i].SetActive(true);
                }
            }
        }
    }
}

