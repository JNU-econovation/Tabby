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
    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        foreach(AnimalData animalData in DataManager._instance.PlayerDatas[GameManager._instance.PlayerIdx].animalDatas)
        {
            Animal temp = Instantiate(animalPrefabs[animalData.idx]).GetComponent<Animal>();
            AnimalManager.AddNewAnimal(temp);
            temp.transform.parent = farmAnimal.transform;
            temp.transform.position = new Vector2(0,0);
            //idx따라 Animal 생성
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
