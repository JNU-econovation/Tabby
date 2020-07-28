using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public GameObject tapzone;
    public GameObject farmObjects;
    public GameObject tapObject;

    public static int farmObjectIndex;
    public static int farmObjectNumber;
    public static int inventorySlotNumber;
    public GameObject putInvenButton;
    public GameObject offPutInvenButton;
    public GameObject putInvenButtonCover;
    public static int oneTapMoney;

    public GameObject map;
    public GameObject arrangeImage;
    public GameObject inventory;

    float maxDistance = 20f;
    Vector2 mousePosition;
    Camera Camera;


    private void Start()
    {
        Camera = GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&& !inventory.activeSelf)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, maxDistance);


            if (hit.collider == null)
            { }
            else if (hit.collider.gameObject != tapzone)
            {
                
                if (hit.collider.gameObject.transform.parent.name == farmObjects.name)
                {
                    FarmObjectController farmObjectController = hit.collider.gameObject.GetComponent<FarmObjectController>();
                    if (farmObjectController.state == FarmObjectController.State.producing&&map.activeSelf==false)
                    {
                        tapObject = hit.collider.gameObject;
                        putInvenButton.SetActive(true);
                        offPutInvenButton.SetActive(true);
                        farmObjectNumber = hit.collider.gameObject.GetComponent<FarmObject>().farmObjectNumber;
                        farmObjectIndex = hit.collider.gameObject.GetComponent<FarmObject>().farmObjectIndex;
                    }
                    farmObjectController.Harvest();
                }
            }
            
            else if(hit.collider.gameObject.name!=putInvenButton.name)
            {
                MoneyManager.MoneyUP(oneTapMoney);
                if (arrangeImage.transform.position.x > -22 && arrangeImage.transform.position.x < 22 && arrangeImage.transform.position.y > -7 && arrangeImage.transform.position.y < 10&& mousePosition.x > -22 && mousePosition.x < 22 && mousePosition.y > -7 && mousePosition.y < 10)
                {
                    Drag drag = arrangeImage.GetComponent<Drag>();
                    drag.ChangePos(mousePosition.x, mousePosition.y);
                }
            }
        }
    }

    public void OffInventoryPutButton()
    {
        putInvenButton.SetActive(false);
        offPutInvenButton.SetActive(false);
    }

    public void PutInventory()
    {
        Spawner._instance.farmObjects[farmObjectIndex].isField = false;
        Destroy(tapObject);
        putInvenButton.SetActive(false);
    }
}
