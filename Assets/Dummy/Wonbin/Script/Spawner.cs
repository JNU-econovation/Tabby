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
    public static List<Animal> animals = new List<Animal>();
    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        PathFinder forInstantiate = new PathFinder();
        foreach (AnimalData animalData in DataManager._instance.PlayerDatas[GameManager._instance.PlayerIdx].animalDatas)
        {
            GameObject newAnimal = Instantiate(animalPrefabs[animalData.idx]);
            Animal temp = newAnimal.GetComponent<Animal>();
            Spawner.AddNewAnimal(temp);
            Rigidbody2D newAnimalRigidbody = newAnimal.GetComponent<Rigidbody2D>();
            newAnimal.transform.parent = farmAnimal.transform;
            forInstantiate.NodeSetting();
            newAnimalRigidbody.position = forInstantiate.RandomSpawnSetting();

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
