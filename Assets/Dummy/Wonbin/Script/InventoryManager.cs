using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryButton;
    public GameObject shopButton;
    public GameObject mapButton;
    public GameObject inventory;
    public GameObject arrangeButton;
    public GameObject saleButton;
    public GameObject putButton;
    public GameObject cancelButton;

    public GameObject inventoryContents;
    public GameObject farmObjects;

    public GameObject arrangeImage;

    //public static int putFarmObNumber;

    public int inventorySlotNum;

    public List<Sprite> invenIcons;
    public List<GameObject> invenItems;

    public void Awake()
    {
        
        foreach (FarmObject farmObject in Spawner.farmObjects)
        {
            if (farmObject.isField == false)
            {
                Image invenSlotImage = inventoryContents.transform.GetChild(inventorySlotNum).gameObject.GetComponent<Image>();
                invenSlotImage.sprite = invenIcons[farmObject.farmObjectNumber];
                FarmObject invenSlotFarmOb = inventoryContents.transform.GetChild(inventorySlotNum).gameObject.GetComponent<FarmObject>();
                invenSlotFarmOb.farmObjectNumber = farmObject.farmObjectNumber;
                invenSlotFarmOb.farmObjectIndex = farmObject.farmObjectIndex;
                inventorySlotNum++;
            }
        }

    }

    public void AddInventory()
    {
        Image invenSlotImage = inventoryContents.transform.GetChild(inventorySlotNum).gameObject.GetComponent<Image>();
        invenSlotImage.sprite = invenIcons[InputManager.farmObjectNumber];
        FarmObject invenSlotFarmOb = inventoryContents.transform.GetChild(inventorySlotNum).gameObject.GetComponent<FarmObject>();
        invenSlotFarmOb.farmObjectNumber = InputManager.farmObjectNumber;
        invenSlotFarmOb.farmObjectIndex = InputManager.farmObjectIndex;
        inventorySlotNum++;
        DataManager._instance.ParseFarmObjectData(Spawner.farmObjects);
    }


    public void OpenInventory()
    {
        inventoryButton.SetActive(false);
        shopButton.SetActive(false);
        mapButton.SetActive(false);
        inventory.SetActive(true);

    }



    public void CloseInventory() 
    {
        inventoryButton.SetActive(true);
        shopButton.SetActive(true);
        mapButton.SetActive(true);
        inventory.SetActive(false);
    }

    public void InvenFarmObTap()
    {
        saleButton.SetActive(true);
        arrangeButton.SetActive(true);
    }

    public void FarmObjectSale()
    {
        Spawner.farmObjects.RemoveAt(InputManager.farmObjectIndex);
        foreach(FarmObject farmObject in Spawner.farmObjects)
        {
            if (farmObject.farmObjectIndex >= InputManager.farmObjectIndex)
                farmObject.farmObjectIndex--;
        }
        for(int i=InputManager.inventorySlotNumber; i < inventorySlotNum; i++)
        {
            
            Image invenSlotImage = inventoryContents.transform.GetChild(i).gameObject.GetComponent<Image>();
            Image nextInvenSlotImage = inventoryContents.transform.GetChild(i+1).gameObject.GetComponent<Image>();
            invenSlotImage.sprite = nextInvenSlotImage.sprite;
            FarmObject invenSlotFarmOb = inventoryContents.transform.GetChild(i).gameObject.GetComponent<FarmObject>();
            FarmObject nextInvenSlotFarmOb = inventoryContents.transform.GetChild(i+1).gameObject.GetComponent<FarmObject>();
            invenSlotFarmOb.farmObjectIndex = nextInvenSlotFarmOb.farmObjectIndex;
            invenSlotFarmOb.farmObjectNumber = nextInvenSlotFarmOb.farmObjectNumber;

        }
        inventorySlotNum--;
        DataManager._instance.ParseFarmObjectData(Spawner.farmObjects);
        MoneyManager.MoneyUP(10);
    }

    public void FarmObjectArrange()
    {
        inventory.SetActive(false);
        saleButton.SetActive(false);
        arrangeButton.SetActive(false);
        putButton.SetActive(true);
        cancelButton.SetActive(true);
        Spawner.farmObjects[InputManager.farmObjectIndex].isField = true;
        Drag arrangeImageDrag = arrangeImage.GetComponent<Drag>();
        arrangeImageDrag.PItransformMid();
        SpriteRenderer spriteRenderer = arrangeImage.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = invenIcons[InputManager.farmObjectNumber];
        for (int i = InputManager.inventorySlotNumber; i <= inventorySlotNum; i++)
        {

            Image invenSlotImage = inventoryContents.transform.GetChild(i).gameObject.GetComponent<Image>();
            Image nextInvenSlotImage = inventoryContents.transform.GetChild(i + 1).gameObject.GetComponent<Image>();
            invenSlotImage.sprite = nextInvenSlotImage.sprite;
            FarmObject invenSlotFarmOb = inventoryContents.transform.GetChild(i).gameObject.GetComponent<FarmObject>();
            FarmObject nextInvenSlotFarmOb = inventoryContents.transform.GetChild(i + 1).gameObject.GetComponent<FarmObject>();
            invenSlotFarmOb.farmObjectIndex = nextInvenSlotFarmOb.farmObjectIndex;
            invenSlotFarmOb.farmObjectNumber = nextInvenSlotFarmOb.farmObjectNumber;

        }
    } 

    public void ArrangeOk()
    {
        inventory.SetActive(true);
        putButton.SetActive(false);
        cancelButton.SetActive(false);
        GameObject arranged = Instantiate(invenItems[InputManager.farmObjectIndex], new Vector2(arrangeImage.transform.position.x, arrangeImage.transform.position.y), Quaternion.identity);
        arranged.transform.parent = farmObjects.transform;
        Spawner.farmObjects[InputManager.farmObjectIndex].isField = true;
        Spawner.farmObjects[InputManager.farmObjectIndex].harvestTime = System.DateTime.Now;
        DataManager._instance.ParseFarmObjectData(Spawner.farmObjects);
    }

    public void ArrangeCencel()
    {
        inventory.SetActive(true);
        putButton.SetActive(false);
        cancelButton.SetActive(false);
    }
}

