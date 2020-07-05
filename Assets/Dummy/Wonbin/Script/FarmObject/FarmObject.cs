using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class FarmObject : MonoBehaviour
{
    public int farmObjectNumber;
    public DateTime harvestTime;
    public float posX;
    public float posY;
    public bool isField;


    public float producePeriod;
    public int moneyOutput;
    //public float atackBuff;
    //public float HPBuff;



    public int farmObjectIdx;

    //public bool harvestable;
    public int shopCost;
    public Sprite farmObjectSprite;
}
