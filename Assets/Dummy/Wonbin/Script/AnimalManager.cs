using Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalManager : MonoBehaviour
{
    public List<Animal> animals;

    List<GameObject> animalObjectList;

    public GameObject readyAnimalList;
    public GameObject animalSlot1;
    public GameObject animalSlot2;
    public GameObject animalSlot3;

    public GameObject ListAnimal;

    public GameObject imgBackupList;
    private Image image;
    public Sprite slotAnimalImg;

    private GameObject slotImg1;
    private GameObject slotImg2;
    private GameObject slotImg3;


    public Text animalListCountText;

    int animalListCount = 2;

    int noamlAnimalCount;
    int yellowAnimalCount;
    int redAnimalCount;
    int indigoAnimalCount;

    /*
    public void AnimalListAdd(GameObject animalObject, GameObject animalButton, string str)
    {
        animalObjectList = new List<GameObject>();//json에서 가져오도록 수정해야함
        animalObjectList.Add(animalObject);
        readyAnimalList = GameObject.Find("readyAnimalList");
        if (readyAnimalList.transform.FindChild(str) == null) {
            Instantiate(animalButton, readyAnimalList.transform.position, readyAnimalList.transform.rotation);
            animalButton.transform.parent = readyAnimalList.transform;
        }
        if (readyAnimalList.transform.FindChild(str) != null)
        {
            animal.animalCount += 1;
        }

    }
    */
   



    public void tapListAnimal()
    {
        if (imgBackupList.transform.childCount != 0 && animalListCount != 0)
        {
            //상황에 맞도록 animalListCount변수 수정해야함
            animalListCount -= 1;
            animalListCountText.text = "" + animalListCount;
            slotImg1 = imgBackupList.transform.GetChild(0).gameObject;
            print(slotImg1.gameObject.name);
            image = slotImg1.GetComponent<Image>();
            image.sprite = slotAnimalImg;
            AnimalManager slotAnimalAnimal = slotImg1.GetComponent<AnimalManager>();
            slotAnimalAnimal.ListAnimal = ListAnimal;
            slotAnimalAnimal.animalListCount = animalListCount;
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
    public void listCountSetting()
    {
        animalListCountText.text = ""+animalListCount;
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

        AnimalManager slotAnimalAnimal = slotImg1.GetComponent<AnimalManager>();
        AnimalManager slotaaanimal = slotAnimalAnimal.ListAnimal.GetComponent<AnimalManager>();
        slotaaanimal.animalListCountUp();

    }
    void animalSlot2Tap()
    {
        slotImg2 = animalSlot2.transform.GetChild(0).gameObject;
        slotImg2.transform.parent = imgBackupList.transform;
        slotImg2.transform.position = slotImg2.transform.parent.position;
        AnimalManager slotAnimalAnimal = slotImg2.GetComponent<AnimalManager>();
        AnimalManager slotaaanimal = slotAnimalAnimal.ListAnimal.GetComponent<AnimalManager>();
        slotaaanimal.animalListCountUp();

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
        AnimalManager slotAnimalAnimal = slotImg3.GetComponent<AnimalManager>();
        AnimalManager slotaaanimal = slotAnimalAnimal.ListAnimal.GetComponent<AnimalManager>();
        slotaaanimal.animalListCountUp();
        slotImg3.transform.parent = imgBackupList.transform;
        slotImg3.transform.position = slotImg3.transform.parent.position;

        if (animalSlot3.transform.childCount != 0)
        {

        }
    }


    void animalListCountUp()
    {
        animalListCount += 1;
        animalListCountText.text = ""+animalListCount;
    }
    void Start()
    {
    }

    void Update()
    {
        
    }
}
