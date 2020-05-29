using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[System.Serializable]
public class Animal : MonoBehaviour
{

    protected int animalNumber;
    private int animalCount;
    protected Sprite babyAnimalSprite;
    protected Sprite growUpSprite;

    public Sprite GetGrowUpSprite() {
        return growUpSprite;
    }
    //동물 들어 옮기기

    
}
//유니티는 왠만하면 스크립트엔 클래스 하나만 하자
