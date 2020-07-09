using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public void SetFarmObjectInfo()
    {
        InputManager.inventorySlotNumber = gameObject.transform.GetSiblingIndex();
        InputManager.farmObjectNumber = gameObject.GetComponent<FarmObject>().farmObjectNumber;
        InputManager.farmObjectIndex = gameObject.GetComponent<FarmObject>().farmObjectIndex;
    }
}
