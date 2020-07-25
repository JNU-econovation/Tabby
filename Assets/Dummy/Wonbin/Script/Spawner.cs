using GameData;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public static Spawner _instance;

    [SerializeField]
    GameObject farmAnimal;
    [SerializeField]
    GameObject farmFarmObject;
    [SerializeField]
    GameObject inventory;
    [SerializeField]
    GameObject child;
    [SerializeField]
    List<GameObject> animalPrefabs;
    [SerializeField]
    List<GameObject> farmObjectPrefabs;
    [SerializeField]
    List<GameObject> farmObjectInvenPrefabs;

    public FarmObject[] farmObjectDictionary;

    public List<Animal> animals=new List<Animal>();
    public List<FarmObject> farmObjects=new List<FarmObject>();

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
        Init();
    }
    public void Init()
    {
        
        InputManager.oneTapMoney = 1;
        PathFinder forInstantiate = new PathFinder();
        
        foreach (FarmObjectData farmObjectData in DataManager._instance.playerData.farmObjectDatas)
        {
            if(farmObjectData.isField==true)
            {
                GameObject newFarmObject = Instantiate(farmObjectPrefabs[farmObjectData.index], new Vector2((float)farmObjectData.posX, (float)farmObjectData.posY), Quaternion.identity);
                FarmObject farmObject = newFarmObject.GetComponent<FarmObject>();
                farmObject.posX = (float)farmObjectData.posX;
                farmObject.posY = (float)farmObjectData.posY;
                farmObject.harvestTime = farmObjectData.harvestTime;
                farmObject.isField = farmObjectData.isField;
                newFarmObject.transform.parent = farmFarmObject.transform;
                farmObject.farmObjectIndex = farmObjects.Count;
                AddNewFarmObject(newFarmObject);
            }
            if (farmObjectData.isField != true)
            {
                GameObject newFarmObject = Instantiate(farmObjectPrefabs[farmObjectData.index], new Vector2((float)farmObjectData.posX, (float)farmObjectData.posY), Quaternion.identity);
                FarmObject farmObject = newFarmObject.GetComponent<FarmObject>();
                farmObject.posX = (float)farmObjectData.posX;
                farmObject.posY = (float)farmObjectData.posY;
                farmObject.harvestTime = farmObjectData.harvestTime;
                farmObject.isField = farmObjectData.isField;
                newFarmObject.transform.parent = farmFarmObject.transform;
                farmObject.farmObjectIndex = farmObjects.Count;
                AddNewFarmObject(newFarmObject);
                Destroy(newFarmObject);
            }

            

        }
        
        forInstantiate.NodeSetting();
        foreach (AnimalData animalData in DataManager._instance.playerData.animalDatas)
        {
            
            GameObject newAnimal = Instantiate(animalPrefabs[animalData.index], forInstantiate.RandomSpawnSetting(), Quaternion.identity);
            Animal newanimal = newAnimal.GetComponent<Animal>();
            newanimal.exp = animalData.exp;
            newanimal.animalName = animalData.animalName;

            //print(temp.animalIdx);

            newAnimal.transform.parent = farmAnimal.transform;
            AddNewAnimal(newAnimal);

            //idx따라 Animal 생성

        }
        
    }

    public void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            MoneyManager.money = 0;
            MoneyManager.heart = 0;
        }
        if (Input.GetKeyDown("m"))
        {
            MoneyManager.money = 10000;
            MoneyManager.heart = 1000;
        }
        if (Input.GetKeyDown("h"))
        {
            MoneyManager.heart +=1;
        }
    }

    public void AddNewAnimal(GameObject animal)
    {
        Animal animalObject = animal.GetComponent<Animal>();
        animalObject.animalIndex = animals.Count;
        animals.Add(animalObject);
        
    }

    public void AddEvolutionAnimal(GameObject animal, int animalsIdx)
    {
        Animal animalObject = animal.GetComponent<Animal>();
        animal.transform.SetSiblingIndex(animalsIdx);
        animalObject.animalIndex = animalsIdx;
        animals[animalObject.animalIndex] =animalObject;
        
    }
    public void BuyNewFarmObject(GameObject farmObject)
    {
        FarmObject farmObjectOb = farmObject.GetComponent<FarmObject>();
        Rigidbody2D farmObjectRB = farmObject.GetComponent<Rigidbody2D>();
        farmObjectOb.posX = farmObjectRB.position.x;
        farmObjectOb.posY = farmObjectRB.position.y;
        farmObjectOb.harvestTime = System.DateTime.Now;
        farmObjects.Add(farmObjectOb);
        print(farmObjects[farmObjects.Count - 1].posX);
    }
    public void AddNewFarmObject(GameObject farmObject)
    {
        FarmObject farmObjectOb = farmObject.GetComponent<FarmObject>();
        Rigidbody2D farmObjectRB = farmObject.GetComponent<Rigidbody2D>();
        farmObjectOb.posX = farmObjectRB.position.x;
        farmObjectOb.posY = farmObjectRB.position.y;
        farmObjects.Add(farmObjectOb);
    }

    public void Evolution(GameObject animal, Spawner spawner)
    {

        Animal animalscript = animal.GetComponent<Animal>();
        int animalNumber = animalscript.animalNumber;
        GameObject evolAnimal;

        if ((animalNumber)%2==0)
        {
            
            evolAnimal=Instantiate(spawner.animalPrefabs[animalNumber + 1], animal.transform.position, Quaternion.identity);
            evolAnimal.transform.parent = farmAnimal.transform;
            evolAnimal.GetComponent<Animal>().animalName = animal.GetComponent<Animal>().animalName;
            AddEvolutionAnimal(evolAnimal, animalscript.animalIndex);
            Destroy(animal);


            DataManager._instance.ParseAnimalDate(animals);
        }

    }




    public void PullToChild()
    {

    }
}
