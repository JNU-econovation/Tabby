using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dionaea : FarmObject
{
    private void Awake()
    {
        farmObjectNumber = 5;
        producePeriod = 60f;
        moneyOutput = 20;

    }
}
