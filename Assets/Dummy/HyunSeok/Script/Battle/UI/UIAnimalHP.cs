using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;

namespace Battle
{
    public class UIAnimalHP : MonoBehaviour
    {
        public int index;

        private AnimalController animalController;
        private AnimalGameData animalData;

        public Image hpBar;
        public Text nameText;

        private void Start()
        {
            if (AnimalManager._instance.animals[index] == null)
                gameObject.SetActive(false);
            if (DataManager._instance.gogoAnimalIndexes[index] != -1)
            {
                animalController = AnimalManager._instance.animals[index];
                animalData = animalController.animalData;
                nameText.text = animalController.animalData.AnimalName;
                CheckWhereAnimal();
            }
            else
            {
                gameObject.SetActive(false);
            }
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

