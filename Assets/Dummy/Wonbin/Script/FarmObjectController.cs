using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmObjectController : MonoBehaviour
{
    FarmObject farmObject;
    float producePeriod;
    float elapsedTime;
    SpriteRenderer spriteRenderer;
    public Sprite producingSprite;
    public Sprite harvestableSprite;
    public bool timePause;
    public GameObject producing;
    public GameObject harvestable;

    void Start()
    {
        //producing = gameObject.transform.GetChild(0).gameObject;
        //harvestable = gameObject.transform.GetChild(1).gameObject;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        farmObject = gameObject.GetComponent<FarmObject>();
        producePeriod = farmObject.producePeriod;
        timePause = false;
        elapsedTime = 0f;
        //producing.gameObject.SetActive(true);
        //harvestable.gameObject.SetActive(false);
        
    }


    void Update()
    {
        if (timePause != true)
        {
            if (elapsedTime < producePeriod)
                elapsedTime += Time.deltaTime;
            else
            {
                //producing.gameObject.SetActive(false);
                timePause = true;
                //harvestable.gameObject.SetActive(true);
                
                spriteRenderer.sprite = harvestableSprite;
            }
        }
    

    }

    private void OnMouseDown()
    {
        if (timePause == true)
        {
            spriteRenderer.sprite = producingSprite;
            MoneyManager.MoneyUP(farmObject.moneyOutput);
            elapsedTime = 0f;
            timePause = false;
        }
    }


}
