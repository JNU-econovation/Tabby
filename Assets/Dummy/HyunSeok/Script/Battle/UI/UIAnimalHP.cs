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
            animalController = AnimalManager._instance.animals[index].GetComponentInChildren<AnimalController>();
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

