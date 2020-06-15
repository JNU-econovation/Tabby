using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEditor;
using System;

public class MapButtonManager : MonoBehaviour
{
    TabbyAnimator tabbyAnimaltor;

    static List<Animal> listAnimals;
    static List<bool> listAnimalAvailability=new List<bool>();

    public GameObject[] animals;

    public GameObject map;
    public GameObject mapButton;
    public GameObject mapCloseButton;
    public GameObject[] mapArea;
    public GameObject ReadyExplore;

    public GameObject readyAnimalList;
    public GameObject[] animalSlot;

    public GameObject ListAnimal;

    public GameObject imgBackupList;
    private Image image;
    Animal slotImgAnimal;
    public Sprite slotAnimalImg;

    private GameObject[] slotImage;

    public static int[] gogoAnimalArray;

    public Text animalListCountText;

    public GameObject mapPrevealImage;
    public Sprite[] areaPrevealSprite;
    private Image prevearChanger;

    private GameObject listAnimal;

    public void openMap()
    {
        map.gameObject.SetActive(true);
        mapButton.gameObject.SetActive(false);
        mapCloseButton.gameObject.SetActive(true);

    }

    public void closeMap()
    {
        if (ReadyExplore.activeSelf == false)
        {
            map.gameObject.SetActive(false);
            mapButton.gameObject.SetActive(true);
            mapCloseButton.gameObject.SetActive(false);
        }
        if (ReadyExplore.activeSelf == true)
        {
            ReadyExplore.gameObject.SetActive(false);
        }
        for(int t=0; t< readyAnimalList.transform.childCount; t++)
            Destroy(readyAnimalList.transform.GetChild(t).gameObject);
        List<Animal> listAnimals = Spawner.animals.ToList();
    }

    public void Area1ReadyOpen()
    {

        listAnimals = Spawner.animals.ToList();
        for (int i = 0; i < listAnimals.Count; i++)
            listAnimalAvailability.Add(true);
        for (int i = 0; i < listAnimals.Count; i++)
            Debug.Log(listAnimals[i].animalNumber);
        for (int i = 0; i < listAnimals.Count; i++)
        {
            int animalNumber = listAnimals[i].animalNumber;
            listAnimal = Instantiate(animals[animalNumber], new Vector2(0, 0), Quaternion.identity);
            listAnimal.transform.parent = readyAnimalList.transform;
            listAnimal.transform.position = listAnimal.transform.parent.position;
            listAnimal.transform.localScale = new Vector2(3.4f, 1.8f);
        }
        ReadyExplore.gameObject.SetActive(true);
        prevearChanger = mapPrevealImage.GetComponent<Image>();
        prevearChanger.sprite = areaPrevealSprite[0];
    }
    public void Area2ReadyOpen()
    {
        listAnimals = Spawner.animals.ToList();
        for (int i = 0; i < listAnimals.Count; i++)
            listAnimalAvailability.Add(true);
        for (int i = 0; i < listAnimals.Count; i++)
        {
            int animalNumber = listAnimals[i].animalNumber;
            listAnimal = Instantiate(animals[animalNumber], new Vector2(0, 0), Quaternion.identity);
            listAnimal.transform.parent = readyAnimalList.transform;
            listAnimal.transform.position = listAnimal.transform.parent.position;
            listAnimal.transform.localScale = new Vector2(3.4f, 1.8f);
        }
        ReadyExplore.gameObject.SetActive(true);
        prevearChanger = mapPrevealImage.GetComponent<Image>();
        prevearChanger.sprite = areaPrevealSprite[1];

    }
    public void Area3ReadyOpen()
    {
        listAnimals = Spawner.animals.ToList();
        for (int i = 0; i < listAnimals.Count; i++)
            listAnimalAvailability.Add(true);
        for (int i = 0; i < listAnimals.Count; i++)
        {
            int animalNumber = listAnimals[i].animalNumber;
            listAnimal = Instantiate(animals[animalNumber], new Vector2(0, 0), Quaternion.identity);
            listAnimal.transform.parent = readyAnimalList.transform;
            listAnimal.transform.position = listAnimal.transform.parent.position;
            listAnimal.transform.localScale = new Vector2(3.4f, 1.8f);
        }
        ReadyExplore.gameObject.SetActive(true);
        prevearChanger = mapPrevealImage.GetComponent<Image>();
        prevearChanger.sprite = areaPrevealSprite[2];

    }



    public void tapListAnimal()
    {
        listAnimals = Spawner.animals.ToList<Animal>();
        Animal thisAnimal = gameObject.GetComponent<Animal>();
        int animalListIdx = transform.GetSiblingIndex();
        SpriteRenderer tapAnimalSprite= gameObject.GetComponent<SpriteRenderer>();

        if (imgBackupList.transform.childCount != 0 && listAnimalAvailability[animalListIdx] != false)
        {

            slotImage[0] = imgBackupList.transform.GetChild(0).gameObject;
            image = slotImage[0].GetComponent<Image>();
            image.sprite = tapAnimalSprite.sprite;
            slotImgAnimal = slotImage[0].GetComponent<Animal>();

            slotImgAnimal.animalIdx = thisAnimal.animalIdx;

            MapButtonManager mapButtonManager = transform.parent.GetComponent<MapButtonManager>();
            animalSlot[0] = mapButtonManager.animalSlot[0];
            animalSlot[1] = mapButtonManager.animalSlot[1];
            animalSlot[2] = mapButtonManager.animalSlot[2];


            if (animalSlot[1].transform.childCount != 0)
            {
                slotImage[0].transform.parent = animalSlot[2].transform;
                slotImage[0].transform.position = slotImage[0].transform.parent.position;
                gogoAnimalArray[2] = animalListIdx;
            }
            if (animalSlot[0].transform.childCount != 0 && animalSlot[1].transform.childCount == 0)
            {
                slotImage[0].transform.parent = animalSlot[1].transform;
                slotImage[0].transform.position = slotImage[0].transform.parent.position;
                gogoAnimalArray[1] = animalListIdx;
                gogoAnimalArray[2] = -1;
            }
            if (animalSlot[0].transform.childCount == 0)
            {
                slotImage[0].transform.parent = animalSlot[0].transform;
                slotImage[0].transform.position = slotImage[0].transform.parent.position;
                gogoAnimalArray[0] = animalListIdx;
                gogoAnimalArray[1] = -1;
                gogoAnimalArray[2] = -1;
            }
        }
    }

    public void slotTap()
    {
        if (transform.parent == animalSlot[2].transform)
            animalSlot3Tap();
        else if (transform.parent == animalSlot[1].transform)
            animalSlot2Tap();
        else if (transform.parent == animalSlot[0].transform)
            animalSlot1Tap();
    }

    void animalSlot1Tap()
    {
        
        slotImage[0] = animalSlot[1].transform.GetChild(0).gameObject;
        slotImage[0].transform.parent = imgBackupList.transform;
        slotImage[0].transform.position = slotImage[0].transform.parent.position;
        Animal slot1Animal = slotImage[0].GetComponent<Animal>();
        Animal slotImg1Animal = listAnimals[slot1Animal.animalIdx];
        listAnimalAvailability[slotImg1Animal.animalIdx] = true;

        
        

        if (animalSlot[1].transform.childCount != 0 && animalSlot[2].transform.childCount == 0)
        {
            slotImage[1] = animalSlot[1].transform.GetChild(0).gameObject;
            slotImage[1].transform.parent = animalSlot[0].transform;
            slotImage[1].transform.position = slotImage[1].transform.parent.position;
            gogoAnimalArray[0] = gogoAnimalArray[1];
            gogoAnimalArray[1] = -1;
        }
        if (animalSlot[2].transform.childCount != 0 && animalSlot[1].transform.childCount != 0)
        {
            slotImage[1] = animalSlot[1].transform.GetChild(0).gameObject;
            slotImage[1].transform.parent = animalSlot[0].transform;
            slotImage[1].transform.position = slotImage[1].transform.parent.position;
            slotImage[2] = animalSlot[2].transform.GetChild(0).gameObject;
            slotImage[2].transform.parent = animalSlot[1].transform;
            slotImage[2].transform.position = slotImage[2].transform.parent.position;

            gogoAnimalArray[1] = gogoAnimalArray[2];
            gogoAnimalArray[0] = gogoAnimalArray[1];
            gogoAnimalArray[2] = -1;
        }

        else
        {
            gogoAnimalArray[0] = -1;
        }

        

    }
    void animalSlot2Tap()
    {
        slotImage[1] = animalSlot[1].transform.GetChild(0).gameObject;
        slotImage[1].transform.parent = imgBackupList.transform;
        slotImage[1].transform.position = slotImage[1].transform.parent.position;
        Animal slot2Animal = slotImage[1].GetComponent<Animal>();
        listAnimalAvailability[slot2Animal.animalIdx] = true;

        


        if (animalSlot[2].transform.childCount != 0)
        {
            slotImage[2] = animalSlot[2].transform.GetChild(0).gameObject;
            slotImage[2].transform.parent = animalSlot[1].transform;
            slotImage[2].transform.position = slotImage[2].transform.parent.position;
            gogoAnimalArray[1] = gogoAnimalArray[2];
            gogoAnimalArray[2] = -1;
        }
        else
        {
            gogoAnimalArray[1] = -1;
        }
    }
    void animalSlot3Tap()
    {
        slotImage[2] = animalSlot[2].transform.GetChild(0).gameObject;
        slotImage[2].transform.parent = imgBackupList.transform;
        slotImage[2].transform.position = slotImage[2].transform.parent.position;
        Animal slot3Animal = slotImage[2].GetComponent<Animal>();
        listAnimalAvailability[slot3Animal.animalIdx] = true;

        gogoAnimalArray[2] = -1;

    }

    public void TapGogoButton()
    {

    }

    public static int[] GetGOGOAnimal()
    {

        return gogoAnimalArray;
    }

}
