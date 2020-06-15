using GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject farmAnimal;
    GameObject child;
    [SerializeField]
    List<GameObject> animalPrefabs;
    List<GameObject> farmObjectPrefabs;
    public static List<Animal> animals=new List<Animal>();
    public static List<FarmObject> farmObjects=new List<FarmObject>();
    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        InputManager.oneTapMoney = 1;
        PathFinder forInstantiate = new PathFinder();
        foreach (AnimalData animalData in DataManager._instance.PlayerDatas[GameManager._instance.PlayerIdx].animalDatas)
        {
            forInstantiate.NodeSetting();
            GameObject newAnimal = Instantiate(animalPrefabs[animalData.idx], forInstantiate.RandomSpawnSetting(),Quaternion.identity);
            Animal temp = newAnimal.GetComponent<Animal>();
            Spawner.AddNewAnimal(temp);
            newAnimal.transform.parent = farmAnimal.transform;

            //idx따라 Animal 생성
            
        }
    }

    public static void AddNewAnimal(Animal animal)
    {

        animal.animalIdx = animals.Count;
        animals.Add(animal);

    }




    //하트생성
    public void MakeChild(GameObject gameObject, GameObject gameObjectPrefabs)
    {
        child = (GameObject)Instantiate(gameObjectPrefabs, gameObject.transform.position, gameObject.transform.rotation);
        child.transform.parent = gameObject.transform;
    }

    public void PullToChild()
    {

    }
}
