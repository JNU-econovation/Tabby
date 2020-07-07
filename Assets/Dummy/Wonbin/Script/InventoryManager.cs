using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryButton;
    public GameObject shopButton;
    public GameObject mapButton;
    public GameObject inventoryCloseButton;
    public GameObject inventory;
    

    public void OpenInventory()
    {
        inventoryButton.SetActive(false);
        shopButton.SetActive(false);
        mapButton.SetActive(false);
        inventoryCloseButton.SetActive(true);
        inventory.SetActive(true);

    }



    public void CloseInventory() 
    {
        inventoryButton.SetActive(true);
        shopButton.SetActive(true);
        mapButton.SetActive(true);
        inventoryCloseButton.SetActive(false);
        inventory.SetActive(false);
    }

    public void invenFarmObTap()
    {

    }
}
