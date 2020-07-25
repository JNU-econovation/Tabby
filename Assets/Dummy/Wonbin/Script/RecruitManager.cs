using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;
public class RecruitManager : MonoBehaviour
{
    public GameObject animalSlot1;
    public GameObject animalSlot2;
    public GameObject animalParent;
    public Text moneyCostText;
    public Text heartCostText;
    public Text moneyText;
    public Text heartText;

    public GameObject namingWindow;
    public GameObject namingImage;
    public GameObject namingInput;
    public GameObject namingOk;

    public static int regionIndex;
    public List<GameObject> animalPrefabs;
    public List<Sprite> animalSprite;

    int slotNum;

    PathFinder forInstantiate;

    public void OnEnable()
    {
        heartText.text = MoneyManager.heart.ToString();
        moneyText.text = MoneyManager.money.ToString();
        moneyCostText.text = "";
        heartCostText.text = "";

        switch (regionIndex)
        {
            case 1:
                Image image1 = animalSlot1.transform.GetChild(1).gameObject.GetComponent<Image>();
                image1.sprite = animalSprite[0];
                Animal animal1= animalSlot1.GetComponent<Animal>();
                animal1.animalNumber = 0;
                animal1.animalCost = 10;
                animal1.animalHeartCost = 4;
                Image image2 = animalSlot2.transform.GetChild(1).gameObject.GetComponent<Image>();
                image2.sprite = animalSprite[2];
                Animal animal2 = animalSlot2.GetComponent<Animal>();
                animal2.animalNumber = 2;
                animal2.animalCost = 40;
                animal2.animalHeartCost = 8;
                break;
            case 2:
                Image image3 = animalSlot1.transform.GetChild(1).gameObject.GetComponent<Image>();
                image3.sprite = animalSprite[4];
                Animal animal3 = animalSlot1.GetComponent<Animal>();
                animal3.animalNumber = 4;
                animal3.animalCost = 150;
                animal3.animalHeartCost = 12;
                Image image4 = animalSlot2.transform.GetChild(1).gameObject.GetComponent<Image>();
                image4.sprite = animalSprite[6];
                Animal animal4 = animalSlot2.GetComponent<Animal>();
                animal4.animalNumber = 6;
                animal4.animalCost = 300;
                animal4.animalHeartCost = 16;
                break;
            case 3:
                Image image5 = animalSlot1.transform.GetChild(1).gameObject.GetComponent<Image>();
                image5.sprite = animalSprite[8];
                Animal animal5 = animalSlot1.GetComponent<Animal>();
                animal5.animalNumber = 8;
                animal5.animalCost = 500;
                animal5.animalHeartCost = 20;
                Image image6 = animalSlot2.transform.GetChild(1).gameObject.GetComponent<Image>();
                image6.sprite = animalSprite[9];
                Animal animal6 = animalSlot2.GetComponent<Animal>();
                animal6.animalNumber = -1;
                break;
        }
    }
    public void TapAnimalSlot1()
    {
        slotNum = 1;
        if (animalSlot1.GetComponent<Animal>().animalNumber != -1)
        {
            moneyCostText.text = animalSlot1.GetComponent<Animal>().animalCost.ToString();
            heartCostText.text = animalSlot1.GetComponent<Animal>().animalHeartCost.ToString();
        }
    }

    public void TapAnimalSlot2()
    {
        slotNum = 2;
        if (animalSlot1.GetComponent<Animal>().animalNumber != -1)
        {
            moneyCostText.text = animalSlot2.GetComponent<Animal>().animalCost.ToString();
            heartCostText.text = animalSlot2.GetComponent<Animal>().animalHeartCost.ToString();
        }
    }

    public void NamingWindowOpen()
    {
        if (int.Parse(moneyText.text) >= int.Parse(moneyCostText.text) && int.Parse(heartText.text) >= int.Parse(heartCostText.text))
        {
            namingWindow.SetActive(true);

            //namingText.i;
            Image image = namingImage.GetComponent<Image>();
            if (slotNum == 1)
                image.sprite = animalSprite[animalSlot1.GetComponent<Animal>().animalNumber];
            if (slotNum == 2)
                image.sprite = animalSprite[animalSlot2.GetComponent<Animal>().animalNumber];
        }
    }




    public void Recruit()
    {

        if ((namingInput.GetComponent<InputField>().text != "")|| (namingInput.GetComponent<InputField>().text != " ") || (namingInput.GetComponent<InputField>().text != "  ")||(namingInput.GetComponent<InputField>().text != "   "))
        {
            if (slotNum == 1)
                RecruitSlot1();
            else if (slotNum == 2)
                RecruitSlot2();
        }
    }
    public void RecruitSlot1()
    {
        forInstantiate = new PathFinder();
        forInstantiate.NodeSetting();
        MoneyManager.MoneyDown(int.Parse(moneyCostText.text));
        MoneyManager.HeartDown(int.Parse(heartCostText.text));
        GameObject newAnimal = Instantiate(animalPrefabs[animalSlot1.GetComponent<Animal>().animalNumber], forInstantiate.RandomSpawnSetting(), Quaternion.identity);
        Animal newanimal = newAnimal.GetComponent<Animal>();
        newAnimal.transform.parent = animalParent.transform;
        newanimal.animalName = namingInput.GetComponent<InputField>().text;
        Spawner._instance.AddNewAnimal(newAnimal);
        DataManager._instance.ParseAnimalDate(Spawner._instance.animals);
        heartText.text = MoneyManager.heart.ToString();
        moneyText.text = MoneyManager.money.ToString();
        namingWindow.SetActive(false);
        namingInput.GetComponent<InputField>().text = "";
    }
    public void RecruitSlot2()
    {
        forInstantiate = new PathFinder();
        forInstantiate.NodeSetting();
        MoneyManager.MoneyDown(int.Parse(moneyCostText.text));
        MoneyManager.HeartDown(int.Parse(heartCostText.text));
        GameObject newAnimal = Instantiate(animalPrefabs[animalSlot2.GetComponent<Animal>().animalNumber], forInstantiate.RandomSpawnSetting(), Quaternion.identity);
        Animal newanimal = newAnimal.GetComponent<Animal>();
        newAnimal.transform.parent = animalParent.transform;
        newanimal.animalName = namingInput.GetComponent<InputField>().text;
        Spawner._instance.AddNewAnimal(newAnimal);
        DataManager._instance.ParseAnimalDate(Spawner._instance.animals);
        heartText.text = MoneyManager.heart.ToString();
        moneyText.text = MoneyManager.money.ToString();
        namingWindow.SetActive(false);
        namingInput.GetComponent<InputField>().text = "";
    }
    public void OnDisable()
    {
        Animal animal1 = animalSlot1.GetComponent<Animal>();
        animal1.animalNumber = -1;
        Animal animal2 = animalSlot1.GetComponent<Animal>();
        animal2.animalNumber = -1;
    }
}
