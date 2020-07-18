using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;
using System;

namespace Battle
{
    public class UIEndPanel : MonoBehaviour
    {
        public Text winText;
        public Text expText;
        public Image farmObjectImage;

        public Sprite farmObjectSpriteNULL;
        public List<Sprite> farmObjectSprites;

        void OnEnable()
        {
            SetPanel();
        }

        void SetPanel()
        {
            // 이김 여부
            if (BattleManager._instance.isWin)
            {
                winText.text = "승리!";
                expText.text = "";
                for (int i = 0; i < 3; i++)
                {
                    if (DataManager._instance.gogoAnimalIndexes[i] == -1)
                    {
                        continue;
                    }
                    if (i != 0)
                        expText.text += ":";
                    expText.text += AnimalManager._instance.animals[i].animalData.AnimalName + " : "
                        + DataManager._instance.playerData.animalDatas[DataManager._instance.gogoAnimalIndexes[i]].exp.ToString()
                        + " + " + EnemyManager._instance.enemy.animalData.enemyExp.ToString()
                        + " / " + AnimalManager._instance.animals[i].animalData.Exp.ToString();
                }
                float rand = UnityEngine.Random.Range(0f, 1f);
                if (rand < EnemyManager._instance.enemy.animalData.farmObjectPercent)
                {
                    if (farmObjectSprites[EnemyManager._instance.enemy.animalData.farmObjectIndex] != null)
                        farmObjectImage.sprite = farmObjectSprites[EnemyManager._instance.enemy.animalData.farmObjectIndex];
                }
                else
                {
                    farmObjectImage.sprite = farmObjectSpriteNULL;
                }
            }
            else
            {
                winText.text = "다음 기회에..";
                expText.text = "";
                for (int i = 0; i < 3; i++)
                {
                    if (DataManager._instance.gogoAnimalIndexes[i] == -1)
                    {
                        continue;
                    }
                    if (i != 0)
                        expText.text += ":";
                    expText.text += AnimalManager._instance.animals[i].animalData.AnimalName + " : "
                        + DataManager._instance.playerData.animalDatas[DataManager._instance.gogoAnimalIndexes[i]].exp.ToString()
                        + " / " + AnimalManager._instance.animals[i].animalData.Exp.ToString();
                }
                farmObjectImage.sprite = farmObjectSpriteNULL;
            }
        }
    }
}

