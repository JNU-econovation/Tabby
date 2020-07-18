using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class UIPausePanel : MonoBehaviour
    {
        public void OnClickPauseButton()
        {
            BattleManager._instance.battleState = BattleDefine.EBattleState.Playing;
            Time.timeScale = 1.0f;
            UIManager._instance.OnPlayingPanel();
        }
    }
}

