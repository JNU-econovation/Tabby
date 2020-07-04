using GameData;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject farmAnimal;
    [SerializeField]
    GameObject child;
    [SerializeField]
    List<GameObject> animalPrefabs;
    [SerializeField]
    List<GameObject> farmObjectPrefabs;

    public FarmObject[] farmObjectDictionary;
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
            //print(temp.animalIdx);
       
            newAnimal.transform.parent = farmAnimal.transform;
            AddNewAnimal(newAnimal);

            //idx따라 Animal 생성

        }
    }

    public static void AddNewAnimal(GameObject animal)
    {
        Animal animalObject = animal.GetComponent<Animal>();
        animalObject.animalIdx = animal.transform.GetSiblingIndex();
        animals.Add(animalObject);
        
    }

    public static void AddEvolutionAnimal(GameObject animal, int animalsIdx)
    {
        Animal animalObject = animal.GetComponent<Animal>();
        animal.transform.SetSiblingIndex(animalsIdx);
        animalObject.animalIdx = animalsIdx;
        animals[animalObject.animalIdx]=animalObject;
        
    }

    public static void AddNewFarmObject(GameObject farmObject)
    {
        FarmObject farmObjectOb = farmObject.GetComponent<FarmObject>();
        Rigidbody2D farmObjectRB = farmObject.GetComponent<Rigidbody2D>();
        farmObjectOb.posX = farmObjectRB.position.x;
        farmObjectOb.posY = farmObjectRB.position.y;
        farmObjectOb.farmObjectIdx = farmObject.transform.GetSiblingIndex();
        farmObjects.Add(farmObjectOb);
        print(farmObjects[farmObjects.Count-1].posX);
    }

    public void Evolution(GameObject animal, Spawner spawner)
    {

        Animal animalscript = animal.GetComponent<Animal>();
        int animalNumber = animalscript.animalNumber;
        GameObject evolAnimal;

        if ((animalNumber+1)%3!=0)
        {
            
            evolAnimal=Instantiate(spawner.animalPrefabs[animalNumber + 1], animal.transform.position, Quaternion.identity);
            evolAnimal.transform.parent = farmAnimal.transform;
            AddEvolutionAnimal(evolAnimal, animalscript.animalIdx);
            Destroy(animal);
            
            DataManager._instance.SaveAnimals(Spawner.animals);
        }

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
