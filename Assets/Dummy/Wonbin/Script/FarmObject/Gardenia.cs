using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gardenia : FarmObject
{
    private void Awake()
    {
        farmObjectNumber = 0;
        producePeriod=10f;
        moneyOutput = 5;
        shopCost = 10;
    }


}
