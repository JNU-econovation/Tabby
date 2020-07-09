using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject tapzone;
    public GameObject farmObjects;
    public GameObject tapObject;

    public static int farmObjectIndex;
    public static int farmObjectNumber;
    public static int inventorySlotNumber;
    public GameObject putInvenButton;
    public static int oneTapMoney;

    float maxDistance = 20f;
    Vector2 mousePosition;
    Camera Camera;


    private void Start()
    {
        Camera = GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, maxDistance);
            

            if (hit.collider == null)
                { }
            else if(hit.collider.gameObject != tapzone)
            {
                if (hit.collider.gameObject.transform.parent.name==farmObjects.name)
                {
                    FarmObjectController farmObjectController = hit.collider.gameObject.GetComponent<FarmObjectController>();
                    if (farmObjectController.state == FarmObjectController.State.producing)
                    {
                        tapObject = hit.collider.gameObject;
                        putInvenButton.SetActive(true);
                        farmObjectNumber= hit.collider.gameObject.GetComponent<FarmObject>().farmObjectNumber;
                        farmObjectIndex = hit.collider.gameObject.GetComponent<FarmObject>().farmObjectIndex;
                    }
                    
                    farmObjectController.Harvest();
                }
            
            
            }

            else if (hit.collider.gameObject.name == putInvenButton.name) { }
            else 
            {
                putInvenButton.SetActive(false);
                MoneyManager.MoneyUP(oneTapMoney);
            }

        }
    }

    public void PutInventory()
    {
        Spawner.farmObjects[farmObjectIndex].isField = false;
        Destroy(tapObject);
    }
}
