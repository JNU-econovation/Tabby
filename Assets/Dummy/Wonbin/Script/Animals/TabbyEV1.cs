using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleDummy;
public class TabbyEV1: Animal
{
    public void Awake()
    {
        animalNumber = 0;
        animalHP = 10;
        animalCost = 20;
        animalHeartCost = 20;

        evolExp = 12;
    }

}
