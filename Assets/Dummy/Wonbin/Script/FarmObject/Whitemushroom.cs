using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whitemushroom : FarmObject
{
    private void Awake()
    {
        farmObjectNumber = 3;
        producePeriod = 3600f;
        moneyOutput = 200;
        shopCost = 1200;

    }
}
