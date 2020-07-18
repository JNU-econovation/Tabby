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
    public GameObject putInvenButtonCover;
    public static int oneTapMoney;

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
                    if (farmObjectController.state == FarmObjectController.State.producing)
                    {
                        tapObject = hit.collider.gameObject;
                        putInvenButton.SetActive(true);
                        farmObjectNumber = hit.collider.gameObject.GetComponent<FarmObject>().farmObjectNumber;
                        farmObjectIndex = hit.collider.gameObject.GetComponent<FarmObject>().farmObjectIndex;
                    }
                    farmObjectController.Harvest();
                }
            }
            
            else if(hit.collider.gameObject.name!=putInvenButton.name)
            {
                Debug.Log("흠흠");
                MoneyManager.MoneyUP(oneTapMoney);
                //putInvenButton.SetActive(false);
            }
        }
    }

    public void PutInventory()
    {
        Debug.Log("인벤에넣기");
        Spawner._instance.farmObjects[farmObjectIndex].isField = false;
        Destroy(tapObject);
        putInvenButton.SetActive(false);
    }
}
