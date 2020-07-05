using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmObjectController : MonoBehaviour
{
    FarmObject farmObject;
    float elapsedTime;
    public enum State {producing, harvestable};
    SpriteRenderer spriteRenderer;
    public Sprite producingSprite;
    public Sprite harvestableSprite;
    State state;
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        farmObject = gameObject.GetComponent<FarmObject>();
        elapsedTime = 0f;
        state = State.producing;//시간 읽어와서 정해지도록 바꿔야함
    }


     private void Update()
    {
        if (state == State.producing)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime < farmObject.producePeriod)
                state = State.harvestable;
            spriteRenderer.sprite = harvestableSprite;
        }
    }

     private void OnMouseDown()
    {
        if (state==State.harvestable)
        {
            spriteRenderer.sprite = producingSprite;
            MoneyManager.MoneyUP(farmObject.moneyOutput);
            state = State.producing;
        }
    }


}
