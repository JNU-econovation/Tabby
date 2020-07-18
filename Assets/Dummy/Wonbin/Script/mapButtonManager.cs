using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEditor;
using System;
using GameData;
using UnityEngine.SceneManagement;

public class MapButtonManager : MonoBehaviour
{
    TabbyAnimator tabbyAnimaltor;

    static List<Animal> listAnimals;
    List<bool> listAnimalAvailability=new List<bool>();

    public static int slot1AnimalIndex;
    public static int slot2AnimalIndex;
    public static int slot3AnimalIndex;

    public GameObject[] animals;

    public GameObject map;
    public GameObject mapButton;
    public GameObject mapCloseButton;
    public GameObject recruitButton;
    public GameObject battleReadyButton;
    public GameObject[] mapArea;

    public GameObject inventoryButton;
    public GameObject putInvenButton;

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

    public static int[] mapGogoAnimalArray;

    public Text animalListCountText;

    public GameObject mapPrevealImage;
    public Sprite[] areaPrevealSprite;
    private Image prevearChanger;


    public GameObject listAnimal;

    public void Awake()
    {
        slot1AnimalIndex = -1;
        slot2AnimalIndex = -1;
        slot3AnimalIndex = -1;
        slotImage = new GameObject[3];
        mapGogoAnimalArray = new int[3];
    }
    public void openMap()
    {
        map.gameObject.SetActive(true);
        mapButton.gameObject.SetActive(false);
        mapCloseButton.gameObject.SetActive(true);
        inventoryButton.SetActive(false);
        putInvenButton.SetActive(false);

    }

    public void closeMap()
    {
        if (battleReadyWindow.activeSelf == false&&recruitWindow.activeSelf==false)
        {
            inventoryButton.SetActive(true);
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
        if (recruitWindow.activeSelf)
        {
            recruitWindow.SetActive(true);
        }
        
        for (int t=0; t< readyAnimalList.transform.childCount; t++)
            Destroy(readyAnimalList.transform.GetChild(t).gameObject);
        List<Animal> listAnimals = Spawner._instance.animals.ToList();
    }

    public void Area1Tap()
    {
        AreaNumber = 0;
        recruitButton.gameObject.SetActive(true);
        battleReadyButton.gameObject.SetActive(true);

        RecruitManager.regionIndex = 1;
        Debug.Log(RecruitManager.regionIndex);
    }
    public void Area2Tap()
    {
        AreaNumber = 1;
        recruitButton.gameObject.SetActive(true);
        battleReadyButton.gameObject.SetActive(true);

        RecruitManager.regionIndex = 2;

        Debug.Log(RecruitManager.regionIndex);
    }
    public void Area3Tap()
    {
        AreaNumber = 2;
        recruitButton.gameObject.SetActive(true);
        battleReadyButton.gameObject.SetActive(true);

        RecruitManager.regionIndex = 3;

        Debug.Log(RecruitManager.regionIndex);
    }

    public void RecruitWindowOpen()
    {
        recruitWindow.gameObject.SetActive(true);
    }
    public void BattleReadyWindowOpen()
    {

        listAnimals = Spawner._instance.animals.ToList();
        for (int i = 0; i < listAnimals.Count; i++)
        {
            listAnimalAvailability.Add(true);
        }
        
        for (int i = 0; i < listAnimals.Count; i++)
        {
            int animalNumber = listAnimals[i].animalNumber;
            listAnimal = Instantiate(animals[animalNumber], new Vector2(0, 0), Quaternion.identity);
            listAnimal.transform.parent = readyAnimalList.transform;
            listAnimal.transform.localScale = new Vector3(5f, 3f, 0f);
            listAnimal.transform.position = listAnimal.transform.parent.position;
            listAnimal.transform.GetChild(2).GetComponent<Text>().text = listAnimals[i].animalName;
            Debug.Log("아우코바"+listAnimals.Count);
        }
        battleReadyWindow.gameObject.SetActive(true);
        prevearChanger = mapPrevealImage.GetComponent<Image>();
        prevearChanger.sprite = areaPrevealSprite[AreaNumber];
    }
    
    public void TapReadyList()
    {
        MapButtonManager mapButtonManager = gameObject.transform.parent.GetComponent<MapButtonManager>();
        mapButtonManager.tapListAnimal(transform.GetSiblingIndex());
    }
    public void tapListAnimal(int index)
    {
        //imgBackupList = GameObject.Find("AnimalImgBackup");
        listAnimals = Spawner._instance.animals.ToList<Animal>();
        Animal thisAnimal = gameObject.transform.GetChild(index).GetComponent<Animal>();
        Sprite tapAnimalSprite= gameObject.transform.GetChild(index).GetComponent<Animal>().animalSprite;
        if (imgBackupList.transform.childCount != 0 && listAnimalAvailability[index] != false)
        {
            slotImage[0] = imgBackupList.transform.GetChild(0).gameObject;
            image = slotImage[0].GetComponent<Image>();
            image.sprite = tapAnimalSprite;
            slotImgAnimal = slotImage[0].GetComponent<Animal>();

            slotImgAnimal.animalIndex = thisAnimal.animalIndex;
            listAnimalAvailability[index] = false;

            if (animalSlot[1].transform.childCount != 0)
            {
                slotImage[0].transform.parent = animalSlot[2].transform;
                slot3AnimalIndex = index;
                slotImage[0].transform.position = slotImage[0].transform.parent.position;
                mapGogoAnimalArray[2] = index;
            }
            if (animalSlot[0].transform.childCount != 0 && animalSlot[1].transform.childCount == 0)
            {
                slotImage[0].transform.parent = animalSlot[1].transform;
                slot2AnimalIndex = index;
                slotImage[0].transform.position = slotImage[0].transform.parent.position;
                mapGogoAnimalArray[1] = index;
                mapGogoAnimalArray[2] = -1;
            }
            if (animalSlot[0].transform.childCount == 0)
            {
                slotImage[0].transform.parent = animalSlot[0].transform;
                slot1AnimalIndex = index;
                slotImage[0].transform.position = slotImage[0].transform.parent.position;
                mapGogoAnimalArray[0] = index;
                mapGogoAnimalArray[1] = -1;
                mapGogoAnimalArray[2] = -1;
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
        listAnimalAvailability[slot1AnimalIndex] = true;
        slotImage[0].transform.parent = imgBackupList.transform;
        slotImage[0].transform.position = slotImage[0].transform.parent.position;
        Animal slot1Animal = slotImage[0].GetComponent<Animal>();
        Animal slotImg1Animal = listAnimals[slot1Animal.animalIndex];
        listAnimalAvailability[slotImg1Animal.animalIndex] = true;

        
        

        if (animalSlot[1].transform.childCount != 0 && animalSlot[2].transform.childCount == 0)
        {
            slotImage[1] = animalSlot[1].transform.GetChild(0).gameObject;
            slot1AnimalIndex = slot2AnimalIndex;
            slotImage[1].transform.parent = animalSlot[0].transform;
            slotImage[1].transform.position = slotImage[1].transform.parent.position;
            mapGogoAnimalArray[0] = mapGogoAnimalArray[1];
            mapGogoAnimalArray[1] = -1;
        }
        if (animalSlot[2].transform.childCount != 0 && animalSlot[1].transform.childCount != 0)
        {
            slotImage[1] = animalSlot[1].transform.GetChild(0).gameObject;
            slot1AnimalIndex = slot2AnimalIndex;
            slotImage[1].transform.parent = animalSlot[0].transform;
            slotImage[1].transform.position = slotImage[1].transform.parent.position;
            slotImage[2] = animalSlot[2].transform.GetChild(0).gameObject;
            slot2AnimalIndex = slot3AnimalIndex;
            slotImage[2].transform.parent = animalSlot[1].transform;
            slotImage[2].transform.position = slotImage[2].transform.parent.position;

            mapGogoAnimalArray[1] = mapGogoAnimalArray[2];
            mapGogoAnimalArray[0] = mapGogoAnimalArray[1];
            mapGogoAnimalArray[2] = -1;
        }

        else
        {
            mapGogoAnimalArray[0] = -1;
        }

        

    }
    void animalSlot2Tap()
    {
        slotImage[1] = animalSlot[1].transform.GetChild(0).gameObject;
        listAnimalAvailability[slot2AnimalIndex] = true;
        slotImage[1].transform.parent = imgBackupList.transform;
        slotImage[1].transform.position = slotImage[1].transform.parent.position;
        Animal slot2Animal = slotImage[1].GetComponent<Animal>();
        listAnimalAvailability[slot2Animal.animalIndex] = true;

        


        if (animalSlot[2].transform.childCount != 0)
        {
            slotImage[2] = animalSlot[2].transform.GetChild(0).gameObject;
            slotImage[2].transform.parent = animalSlot[1].transform;
            slot2AnimalIndex = slot3AnimalIndex;
            slotImage[2].transform.position = slotImage[2].transform.parent.position;
            mapGogoAnimalArray[1] = mapGogoAnimalArray[2];
            mapGogoAnimalArray[2] = -1;
        }
        else
        {
            mapGogoAnimalArray[1] = -1;
        }
    }
    void animalSlot3Tap()
    {
        slotImage[2] = animalSlot[2].transform.GetChild(0).gameObject;
        listAnimalAvailability[slot3AnimalIndex] = true;
        slotImage[2].transform.parent = imgBackupList.transform;
        slotImage[2].transform.position = slotImage[2].transform.parent.position;
        Animal slot3Animal = slotImage[2].GetComponent<Animal>();
        listAnimalAvailability[slot3Animal.animalIndex] = true;

        mapGogoAnimalArray[2] = -1;
        
    }

    public void TapGogoButton()
    {

        mapGogoAnimalArray[0] = slot1AnimalIndex;
        mapGogoAnimalArray[1] = slot2AnimalIndex;
        mapGogoAnimalArray[2] = slot3AnimalIndex;
        Debug.Log("디버그로그");
        Debug.Log(mapGogoAnimalArray[0]);
        Debug.Log(mapGogoAnimalArray[1]);
        Debug.Log(mapGogoAnimalArray[2]);
        DataManager._instance.gogoAnimalIndexes = mapGogoAnimalArray;
        DataManager._instance.regionIndex = AreaNumber;
        SceneManager.LoadScene("HS_Battle");
    }

   

}
