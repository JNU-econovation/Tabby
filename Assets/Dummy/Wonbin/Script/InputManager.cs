using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject tapzone;
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
            //Debug.Log(hit.collider.gameObject.name);
            if(hit != null && hit.collider.gameObject != tapzone) { }
            else if(hit == null){ }
            else 
            {
                MoneyManager.MoneyUP(oneTapMoney);
            }
        }
    }
}
