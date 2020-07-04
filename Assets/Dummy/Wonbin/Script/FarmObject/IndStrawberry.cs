using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndStrawberry : FarmObject
{
    private void Awake()
    {
        farmObjectNumber = 1;
        producePeriod = 60f;
        moneyOutput = 20;

    }
}
