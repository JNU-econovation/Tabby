using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;
using System;

public class FarmObjectController : MonoBehaviour
{
    FarmObject farmObject;
    public float elapsedTime;
    public enum State {producing, harvestable};
    SpriteRenderer spriteRenderer;
    public Sprite producingSprite;
    public Sprite harvestableSprite;
    public State state;
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        farmObject = gameObject.GetComponent<FarmObject>();
        elapsedTime = 0f;
        
        System.TimeSpan conpareTime = System.DateTime.Now - farmObject.harvestTime;
        if (conpareTime.TotalSeconds > farmObject.producePeriod)
        {
            state = State.harvestable;
            spriteRenderer.sprite = harvestableSprite;
        }
        else
        {
            state = State.producing;
            elapsedTime += (float)conpareTime.TotalSeconds;
        }
    }


     private void Update()
    {
        //System.TimeSpan conpareTime = System.DateTime.Now - farmObject.harvestTime;
        //Debug.LogFormat("" + conpareTime.TotalSeconds);
        if (state == State.producing)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > farmObject.producePeriod)
            {
                state = State.harvestable;
                spriteRenderer.sprite = harvestableSprite;
            }
        }
    }

     public void Harvest()
    {
        if (state==State.harvestable)
        {
            elapsedTime = 0f;
            MoneyManager.MoneyUP(farmObject.moneyOutput);
            state = State.producing;
            spriteRenderer.sprite = producingSprite;
            farmObject.harvestTime = System.DateTime.Now;
            DataManager._instance.ParseFarmObjectData(Spawner._instance.farmObjects);
        }
    }


}
