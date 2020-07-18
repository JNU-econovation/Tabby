using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleDummy;

public class TabbyEV2: Animal
{
    public void Awake()
    {
        animalNumber = 1;
        animalHP = 100;
        animalCost = 100;

        evolExp = 1000000;
        exp = 5;
    }


}



