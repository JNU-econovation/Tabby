using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;

namespace Battle
{
    public class UIEnemyHP : MonoBehaviour
    {
        private AnimalController animalController;
        private AnimalGameData animalData;

        public Image hpBar;
        public Text nameText;

        private void Start()
        {
            animalController = EnemyManager._instance.enemy.GetComponentInChildren<AnimalController>();
            animalData = animalController.animalData;

            nameText.text = animalController.animalData.AnimalName;
            CheckWhereAnimal();
        }

        private void CheckWhereAnimal()
        {
            animalData.EvHP += UpdateHP;
        }
        private void UpdateHP()
        {
            hpBar.fillAmount = animalData.HP / animalData.MaxHP;
        }
    }
}

