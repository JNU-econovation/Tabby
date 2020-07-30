using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitCrab : FarmObject
{
    private void Awake()
    {
        farmObjectNumber = 4;
        producePeriod = 60f;
        moneyOutput = 30;

    }
}
