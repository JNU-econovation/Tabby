using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;

namespace Battle
{
    public class UIAnimalHP : MonoBehaviour
    {
        public List<Text> hpList;

        private void Start()
        {
            CheckWhereAnimal();
        }

        private void CheckWhereAnimal()
        {
            for (int i = 0; i < 3; i++)
            {
                //int animalIndex = DataManager._instance.gogoAnimalIndexes[i];
                int animalIndex = 0;
                if (animalIndex == -1)
                {
                    hpList[i].gameObject.SetActive(false);
                }
                else
                {
                    hpList[i].gameObject.SetActive(true);
                    BattleManager._instance.animalManager.animals[i].animalData.EvHP += UpdateHP;
                    hpList[i].text = BattleManager._instance.animalManager.animals[i].animalData.HP.ToString();
                }
            }
        }
        private void UpdateHP(int index)
        {
            hpList[index].text = BattleManager._instance.animalManager.animals[index].animalData.HP.ToString();

        }
    }
}

