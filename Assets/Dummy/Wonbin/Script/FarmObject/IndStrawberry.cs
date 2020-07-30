using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndStrawberry : FarmObject
{
    private void Awake()
    {
        farmObjectNumber = 1;
        producePeriod = 15f;
        moneyOutput = 15;
        shopCost = 100;

    }
}
