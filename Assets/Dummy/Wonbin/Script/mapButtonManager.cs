using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEditor;

public class MapButtonManager : MonoBehaviour
{
    TabbyAnimator tabbyAnimaltor = new TabbyAnimator();

    static List<Animal> listAnimals=AnimalManager.animals.ToList<Animal>();

    public GameObject blue;
    public GameObject yellow;
    public GameObject red;
    public GameObject indigo;

    public GameObject map;
    public GameObject mapButton;
    public GameObject mapCloseButton;
    public GameObject mapArea1;
    public GameObject mapArea2;
    public GameObject mapArea3;
    public GameObject ReadyExplore;

    public GameObject readyAnimalList;
    public GameObject animalSlot1;
    public GameObject animalSlot2;
    public GameObject animalSlot3;

    public GameObject ListAnimal;

    public GameObject imgBackupList;
    private Image image;
    Animal slotImgAnimal;
    public Sprite slotAnimalImg;

    private GameObject slotImg1;
    private GameObject slotImg2;
    private GameObject slotImg3;


    public Text animalListCountText;

    public GameObject mapPrevealImage;
    public Sprite area1PrevealSprite;
    public Sprite area2PrevealSprite;
    public Sprite area3PrevealSprite;
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
        List<Animal> listAnimals = AnimalManager.animals.ToList();
    }

    public void Area1ReadyOpen()
    {
        List<Animal> listAnimals = AnimalManager.animals.ToList();
        for(int i = 0; i < listAnimals.Count; i++)
        {
            Animal a = listAnimals[i];
            if (a.animalNumber == 1)
            {
                listAnimal=Instantiate(blue, new Vector2(0,0), Quaternion.identity);
                listAnimal.transform.parent = readyAnimalList.transform;
                listAnimal.transform.position = listAnimal.transform.parent.position;
                listAnimal.transform.localScale = new Vector2(3.4f, 1.8f);
            }
            else if (a.animalNumber == 2)
            {
                listAnimal = Instantiate(yellow, new Vector2(0, 0), Quaternion.identity);
                listAnimal.transform.parent = readyAnimalList.transform;
                listAnimal.transform.position = listAnimal.transform.parent.position;
                listAnimal.transform.localScale = new Vector2(3.4f, 1.8f);
            }

            else if (a.animalNumber == 3)
            {
                listAnimal = Instantiate(red, new Vector2(0, 0), Quaternion.identity);
                listAnimal.transform.parent = readyAnimalList.transform;
                listAnimal.transform.position = listAnimal.transform.parent.position;
                listAnimal.transform.localScale = new Vector2(3.4f, 1.8f);
            }

            else
            {
                listAnimal = Instantiate(indigo, new Vector2(0, 0), Quaternion.identity);
                listAnimal.transform.parent = readyAnimalList.transform;
                listAnimal.transform.position = listAnimal.transform.parent.position;
                listAnimal.transform.localScale = new Vector2(3.4f, 1.8f);
            }
            //animal추가시 수정해야함


            Text listCount = listAnimal.transform.GetChild(2).gameObject.GetComponent<Text>();
            listCount.text = ""+listAnimals[i].animalCount;

        }
        ReadyExplore.gameObject.SetActive(true);
        prevearChanger = mapPrevealImage.GetComponent<Image>();
        prevearChanger.sprite = area1PrevealSprite;
    }
    public void Area2ReadyOpen()
    {
        List<Animal> listAnimals = AnimalManager.animals.ToList();
        for (int i = 0; i < listAnimals.Count; i++)
        {
            Animal a = listAnimals[i];
            if (a.animalNumber == 1)
            {
                listAnimal = Instantiate(blue, new Vector2(0, 0), Quaternion.identity);
                listAnimal.transform.parent = readyAnimalList.transform;
                listAnimal.transform.position = listAnimal.transform.parent.position;
                listAnimal.transform.localScale = new Vector2(3.4f, 1.8f);
            }
            else if (a.animalNumber == 2)
            {
                listAnimal = Instantiate(yellow, new Vector2(0, 0), Quaternion.identity);
                listAnimal.transform.parent = readyAnimalList.transform;
                listAnimal.transform.position = listAnimal.transform.parent.position;
                listAnimal.transform.localScale = new Vector2(3.4f, 1.8f);
            }

            else if (a.animalNumber == 3)
            {
                listAnimal = Instantiate(red, new Vector2(0, 0), Quaternion.identity);
                listAnimal.transform.parent = readyAnimalList.transform;
                listAnimal.transform.position = listAnimal.transform.parent.position;
                listAnimal.transform.localScale = new Vector2(3.4f, 1.8f);
            }

            else
            {
                listAnimal = Instantiate(indigo, new Vector2(0, 0), Quaternion.identity);
                listAnimal.transform.parent = readyAnimalList.transform;
                listAnimal.transform.position = listAnimal.transform.parent.position;
                listAnimal.transform.localScale = new Vector2(3.4f, 1.8f);
            }
            //animal추가시 수정해야함


            Text listCount = listAnimal.transform.GetChild(2).gameObject.GetComponent<Text>();
            listCount.text = "" + listAnimals[i].animalCount;

        }
        ReadyExplore.gameObject.SetActive(true);
        prevearChanger = mapPrevealImage.GetComponent<Image>();
        prevearChanger.sprite = area2PrevealSprite;

    }
    public void Area3ReadyOpen()
    {
        List<Animal> listAnimals = AnimalManager.animals.ToList();
        for (int i = 0; i < listAnimals.Count; i++)
        {
            Animal a = listAnimals[i];
            if (a.animalNumber == 1)
            {
                listAnimal = Instantiate(blue, new Vector2(0, 0), Quaternion.identity);
                listAnimal.transform.parent = readyAnimalList.transform;
                listAnimal.transform.position = listAnimal.transform.parent.position;
                listAnimal.transform.localScale = new Vector2(3.4f, 1.8f);
            }
            else if (a.animalNumber == 2)
            {
                listAnimal = Instantiate(yellow, new Vector2(0, 0), Quaternion.identity);
                listAnimal.transform.parent = readyAnimalList.transform;
                listAnimal.transform.position = listAnimal.transform.parent.position;
                listAnimal.transform.localScale = new Vector2(3.4f, 1.8f);
            }

            else if (a.animalNumber == 3)
            {
                listAnimal = Instantiate(red, new Vector2(0, 0), Quaternion.identity);
                listAnimal.transform.parent = readyAnimalList.transform;
                listAnimal.transform.position = listAnimal.transform.parent.position;
                listAnimal.transform.localScale = new Vector2(3.4f, 1.8f);
            }

            else
            {
                listAnimal = Instantiate(indigo, new Vector2(0, 0), Quaternion.identity);
                listAnimal.transform.parent = readyAnimalList.transform;
                listAnimal.transform.position = listAnimal.transform.parent.position;
                listAnimal.transform.localScale = new Vector2(3.4f, 1.8f);
            }
            //animal추가시 수정해야함


            Text listCount = listAnimal.transform.GetChild(2).gameObject.GetComponent<Text>();
            listCount.text = "" + listAnimals[i].animalCount;

        }
        ReadyExplore.gameObject.SetActive(true);
        prevearChanger = mapPrevealImage.GetComponent<Image>();
        prevearChanger.sprite = area3PrevealSprite;

    }



    public void tapListAnimal()
    {
        listAnimals = AnimalManager.animals.ToList<Animal>();
        Animal thisAnimal = gameObject.GetComponent<Animal>();
        Animal animalReadyList = listAnimals.Find(i => i.animalNumber==thisAnimal.animalNumber);

        imgBackupList = GameObject.Find("AnimalImgBackup");

        if (imgBackupList.transform.childCount != 0 && animalReadyList.animalCount != 0)
        {
            //상황에 맞도록 animalListCount변수 수정해야함
            animalReadyList.animalCount -= 1;
            animalListCountText.text = "" + animalReadyList.animalCount;
            
            slotImg1 = imgBackupList.transform.GetChild(0).gameObject;
            //print(slotImg1.gameObject.name);
            image = slotImg1.GetComponent<Image>();
            image.sprite = animalReadyList.babyAnimalSprite;
            slotImgAnimal = slotImg1.GetComponent<Animal>();

            slotImgAnimal.animalNumber = animalReadyList.animalNumber;
            //slotImgAnimal.animalCount = listAnimals[animalReadyListNumber].animalCount;

            MapButtonManager mapButtonManager = transform.parent.GetComponent<MapButtonManager>();
            animalSlot1 = mapButtonManager.animalSlot1;
            animalSlot2 = mapButtonManager.animalSlot2;
            animalSlot3 = mapButtonManager.animalSlot3;


            if (animalSlot2.transform.childCount != 0)
            {
                slotImg1.transform.parent = animalSlot3.transform;
                slotImg1.transform.position = slotImg1.transform.parent.position;
            }
            if (animalSlot1.transform.childCount != 0 && animalSlot2.transform.childCount == 0)
            {
                slotImg1.transform.parent = animalSlot2.transform;
                slotImg1.transform.position = slotImg1.transform.parent.position;
            }
            if (animalSlot1.transform.childCount == 0)
            {
                slotImg1.transform.parent = animalSlot1.transform;
                slotImg1.transform.position = slotImg1.transform.parent.position;
            }
        }
    }


    //public void listCountSetting(int animalReadyListNumber)
    //{
    //    animalListCountText.text = "" + listAnimals[animalReadyListNumber].animalCount;
    //}

    public int GetAnimalNumber()
    {
        Animal aanimal = gameObject.GetComponent<Animal>();
        return aanimal.animalNumber;
    }

    public void slotTap()
    {
        if (transform.parent == animalSlot3.transform)
            animalSlot3Tap();
        else if (transform.parent == animalSlot2.transform)
            animalSlot2Tap();
        else if (transform.parent == animalSlot1.transform)
            animalSlot1Tap();
    }

    void animalSlot1Tap()
    {
        
        slotImg1 = animalSlot1.transform.GetChild(0).gameObject;
        slotImg1.transform.parent = imgBackupList.transform;
        slotImg1.transform.position = slotImg1.transform.parent.position;
        Animal slot1Animal = slotImg1.GetComponent<Animal>();
        Animal slotImg1Animal = listAnimals.Find(i => i.animalNumber == slot1Animal.animalNumber);
        slotImg1Animal.animalCount += 1;

        Animal animalReadyList = listAnimals.Find(i => i.animalNumber == slot1Animal.animalNumber);
        Transform[] readyAnimalListObject = readyAnimalList.gameObject.GetComponentsInChildren<Transform>();
        Animal[] readyAnimalListlist = readyAnimalList.gameObject.GetComponentsInChildren<Animal>();
        int forCount = ArrayUtility.FindIndex(readyAnimalListlist, i => i.animalNumber == slot1Animal.animalNumber);
        readyAnimalListlist[forCount].animalCount -= 1;
        Text mane = readyAnimalListlist[forCount].transform.GetChild(2).gameObject.GetComponent<Text>();
        mane.text = "" + slotImg1Animal.animalCount;

        if (animalSlot2.transform.childCount != 0 && animalSlot3.transform.childCount == 0)
        {
            slotImg2 = animalSlot2.transform.GetChild(0).gameObject;
            slotImg2.transform.parent = animalSlot1.transform;
            slotImg2.transform.position = slotImg2.transform.parent.position;
        }
        if (animalSlot3.transform.childCount != 0 && animalSlot2.transform.childCount != 0)
        {
            slotImg2 = animalSlot2.transform.GetChild(0).gameObject;
            slotImg2.transform.parent = animalSlot1.transform;
            slotImg2.transform.position = slotImg2.transform.parent.position;
            slotImg3 = animalSlot3.transform.GetChild(0).gameObject;
            slotImg3.transform.parent = animalSlot2.transform;
            slotImg3.transform.position = slotImg3.transform.parent.position;
        }

        

    }
    void animalSlot2Tap()
    {
        slotImg2 = animalSlot2.transform.GetChild(0).gameObject;
        slotImg2.transform.parent = imgBackupList.transform;
        slotImg2.transform.position = slotImg2.transform.parent.position;
        Animal slot2Animal = slotImg2.GetComponent<Animal>();
        Animal slotImg2Animal = listAnimals.Find(i => i.animalNumber == slot2Animal.animalNumber);
        slotImg2Animal.animalCount += 1;

        Animal animalReadyList = listAnimals.Find(i => i.animalNumber == slot2Animal.animalNumber);
        Transform[] readyAnimalListObject = readyAnimalList.gameObject.GetComponentsInChildren<Transform>();
        Animal[] readyAnimalListlist = readyAnimalList.gameObject.GetComponentsInChildren<Animal>();
        int forCount = ArrayUtility.FindIndex(readyAnimalListlist, i => i.animalNumber == slot2Animal.animalNumber);
        readyAnimalListlist[forCount].animalCount -= 1;
        Text mane = readyAnimalListlist[forCount].transform.GetChild(2).gameObject.GetComponent<Text>();
        mane.text = "" + slotImg2Animal.animalCount;


        if (animalSlot3.transform.childCount != 0)
        {
            slotImg3 = animalSlot3.transform.GetChild(0).gameObject;
            slotImg3.transform.parent = animalSlot2.transform;
            slotImg3.transform.position = slotImg3.transform.parent.position;
        }
    }
    void animalSlot3Tap()
    {
        slotImg3 = animalSlot3.transform.GetChild(0).gameObject;
        slotImg3.transform.parent = imgBackupList.transform;
        slotImg3.transform.position = slotImg3.transform.parent.position;
        Animal slot3Animal = slotImg3.GetComponent<Animal>();
        Animal slotImg3Animal = listAnimals.Find(i => i.animalNumber == slot3Animal.animalNumber);
        slotImg3Animal.animalCount += 1;

        Animal animalReadyList = listAnimals.Find(i => i.animalNumber == slot3Animal.animalNumber);
        Transform[] readyAnimalListObject = readyAnimalList.gameObject.GetComponentsInChildren<Transform>();
        Animal[] readyAnimalListlist = readyAnimalList.gameObject.GetComponentsInChildren<Animal>();
        int forCount = ArrayUtility.FindIndex(readyAnimalListlist, i => i.animalNumber == slot3Animal.animalNumber);
        readyAnimalListlist[forCount].animalCount -= 1;
        Text mane = readyAnimalListlist[forCount].transform.GetChild(2).gameObject.GetComponent<Text>();
        mane.text = "" + slotImg3Animal.animalCount;

        if (animalSlot3.transform.childCount != 0)
        {

        }
    }

}
