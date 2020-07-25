using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ginseng : FarmObject
{
    private void Awake()
    {
        farmObjectNumber = 2;
        producePeriod = 60f;
        moneyOutput = 50;
        shopCost = 500;

    }
}
