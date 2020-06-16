using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;
using UnityEditor;

public class SeaDogEV1 : Animal
{

    public void Awake()
    {
        animalNumber = 3;
        animalHP = 10000;
        animalCost = 600;
    }
}
