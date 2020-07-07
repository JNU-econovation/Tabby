using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

namespace Battle
{
    public class AnimalManager : MonoBehaviour
    {
        public static AnimalManager _instance;
        public List<AnimalController> animals;
        public List<Transform> animalPos;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(gameObject);
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
                animals.Add(animal.transform.GetChild(0).GetComponent<AnimalController>());
                animals[i].animalData.BattleIndex = i;
                animal.transform.position = animalPos[i].transform.position;
            }
        }
    }
}

