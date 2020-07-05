using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject tapzone;
    public GameObject farmObjects;
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
                    farmObjectController.Harvest();
                }
            
            
            }
            
            else 
            {
                MoneyManager.MoneyUP(oneTapMoney);
            }

        }
    }
}
