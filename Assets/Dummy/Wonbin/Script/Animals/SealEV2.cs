using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleDummy;
using UnityEditor;

public class SealEV2 : Animal
{

    public void Awake()
    {
        animalNumber = 3;
        animalHP = 10000;
        animalCost = 600;

        evolExp = 1000000;
    }
}
