using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

//[System.Serializable]
public class Animal : MonoBehaviour
{
    public int animalNumber;

    protected int animalHP ;
    public int animalCost;

    public int level;

    public int animalCount;//옮기자

    public Sprite babyAnimalSprite;
    public Sprite middleAnimalSprite;
    public Sprite growUpSprite;

    public Sprite GetGrowUpSprite() {
        return growUpSprite;
    }
    
}
//유니티는 왠만하면 스크립트엔 클래스 하나만 하자
