using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

namespace Battle
{
    public class AnimalManager : MonoBehaviour
    {
        public List<AnimalController> animals;
        public List<Transform> animalPos;

        private void Awake()
        {
            animals = new List<AnimalController>(3);
            SpawnAnimalUseData();
        }

        // 
        public void SpawnAnimalUseData()
        {
            for (int i = 0; i < 3; i++)
            {
                //int animalIndex = DataManager._instance.gogoAnimalIndexes[i];
                int animalIndex = 0;
                // 만약 -1 일 경우 소환 안함
                if (animalIndex == -1)
                {
                    animals.Add(null);
                    continue;
                }
                GameObject animal = Instantiate(Resources.Load("Battle/Animal/Prefab_Animal_" + animalIndex) as GameObject);
                if (animal == null)
                {
                    animals.Add(null);
                    continue;
                }
                animals.Add(null);
                animals.Add(animal.transform.GetChild(0).GetComponent<AnimalController>());
                animal.transform.position = animalPos[i].transform.position;
            }
        }
    }
}

