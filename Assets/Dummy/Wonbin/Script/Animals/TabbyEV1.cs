using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;
public class TabbyEV1: Animal
{
    public void Awake()
    {
        animalIdx = 0;
        animalNumber = 0;
        animalHP = 10;
        animalCost = 20;

        exp = 3;
    }

}
