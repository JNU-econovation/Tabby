using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;

namespace Battle
{
    public class UIAnimalHP : MonoBehaviour
    {
        public AnimalController animalController;
        public AnimalGameData animalData;

        public Image hpBar;

        private void Start()
        {
            animalData = animalController.animalData;
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

