using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEditor;
using System;
using GameData;

public class MapButtonManager : MonoBehaviour
{
    TabbyAnimator tabbyAnimaltor;

    static List<Animal> listAnimals;
    static List<bool> listAnimalAvailability=new List<bool>();

    public GameObject[] animals;

    public GameObject map;
    public GameObject mapButton;
    public GameObject mapCloseButton;
    public GameObject recruitButton;
    public GameObject battleReadyButton;
    public GameObject[] mapArea;

    public static int AreaNumber;

    public GameObject battleReadyWindow;
    public GameObject recruitWindow;

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

    public void Awake()
    {
        slotImage = new GameObject[3];
        gogoAnimalArray = new int[3];
    }
    public void openMap()
    {
        map.gameObject.SetActive(true);
        mapButton.gameObject.SetActive(false);
        mapCloseButton.gameObject.SetActive(true);

    }

    public void closeMap()
    {
        if (battleReadyWindow.activeSelf == false&&recruitWindow.activeSelf==false)
        {
            map.gameObject.SetActive(false);
            mapButton.gameObject.SetActive(true);
            mapCloseButton.gameObject.SetActive(false);
        }
        if (recruitButton.activeSelf == true)
        {
            recruitButton.gameObject.SetActive(false);
            recruitWindow.gameObject.SetActive(false);
            battleReadyButton.gameObject.SetActive(false);
            battleReadyWindow.gameObject.SetActive(false);
        }
        for(int t=0; t< readyAnimalList.transform.childCount; t++)
            Destroy(readyAnimalList.transform.GetChild(t).gameObject);
        List<Animal> listAnimals = Spawner.animals.ToList();
    }

    public void Area1Tap()
    {
        AreaNumber = 0;
        recruitButton.gameObject.SetActive(true);
        battleReadyButton.gameObject.SetActive(true);
    }
    public void Area2Tap()
    {
        AreaNumber = 1;
        recruitButton.gameObject.SetActive(true);
        battleReadyButton.gameObject.SetActive(true);
    }
    public void Area3Tap()
    {
        AreaNumber = 2;
        recruitButton.gameObject.SetActive(true);
        battleReadyButton.gameObject.SetActive(true);
    }

    public void RecruitWindowOpen()
    {
        recruitWindow.gameObject.SetActive(true);
    }
    public void BattleReadyWindowOpen()
    {

        listAnimals = Spawner.animals.ToList();
        for (int i = 0; i < listAnimals.Count; i++)
        {
            listAnimalAvailability = new List<bool>();
            listAnimalAvailability.Add(true);
        }
        for (int i = 0; i < listAnimals.Count; i++)
            Debug.Log(listAnimals[i].animalNumber);
        
        for (int i = 0; i < listAnimals.Count; i++)
        {
            int animalNumber = listAnimals[i].animalNumber;
            listAnimal = Instantiate(animals[animalNumber], new Vector2(0, 0), Quaternion.identity);
            listAnimal.transform.parent = readyAnimalList.transform;
            listAnimal.transform.localScale = new Vector3(5f, 3f, 0f);
            listAnimal.transform.position = listAnimal.transform.parent.position;
        }
        battleReadyWindow.gameObject.SetActive(true);
        prevearChanger = mapPrevealImage.GetComponent<Image>();
        prevearChanger.sprite = areaPrevealSprite[AreaNumber];
    }
    



    public void tapListAnimal()
    {
        listAnimals = Spawner.animals.ToList<Animal>();
        Animal thisAnimal = gameObject.GetComponent<Animal>();
        int animalListIdx = transform.GetSiblingIndex();
        Sprite tapAnimalSprite= gameObject.GetComponent<Animal>().animalSprite;


        if (imgBackupList.transform.childCount != 0 && listAnimalAvailability[animalListIdx] != false)
        {
            print(imgBackupList.transform.childCount+"마리");
            slotImage[0] = imgBackupList.transform.GetChild(0).gameObject;
            image = slotImage[0].GetComponent<Image>();
            image.sprite = tapAnimalSprite;
            slotImgAnimal = slotImage[0].GetComponent<Animal>();

            slotImgAnimal.animalIndex = thisAnimal.animalIndex;

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
        
        slotImage[0] = animalSlot[0].transform.GetChild(0).gameObject;
        slotImage[0].transform.parent = imgBackupList.transform;
        slotImage[0].transform.position = slotImage[0].transform.parent.position;
        Animal slot1Animal = slotImage[0].GetComponent<Animal>();
        Animal slotImg1Animal = listAnimals[slot1Animal.animalIndex];
        listAnimalAvailability[slotImg1Animal.animalIndex] = true;

        
        

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
        listAnimalAvailability[slot2Animal.animalIndex] = true;

        


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
        listAnimalAvailability[slot3Animal.animalIndex] = true;

        gogoAnimalArray[2] = -1;
        
    }

    public void TapGogoButton()
    {
        DataManager._instance.gogoAnimalIndexes = gogoAnimalArray;
    }

    public static int[] GetGOGOAnimal()
    {

        return gogoAnimalArray;
    }

}
