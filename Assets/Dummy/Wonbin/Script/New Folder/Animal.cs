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
    void OnMouseDrag()
    {
        if (timeAfterHeart <= heartRate && timeAfterHeart>=1.3)//하트획득시 동물이 들리지 않도록 조절
            Drag.AnimalDrag(animalrigidbody);
    }



    //동물을 내려 놓았을 때
    private void OnMouseUp()
    {
        //다시 랜덤지정으로 길찾기 시작
        if (timeAfterHeart <= heartRate && timeAfterHeart >= 1.3)
        {
            pathfinder.ReFinding(animalrigidbody, 6);
            //애니메이션 돌아옴
            animator.SetBool("tapAnimal", false);
        }
    }

    //동물에 하트가 있다면 하트획득
    private void OnMouseDown()
    {
        if (transform.childCount != 0)
        {
            MoneyManager.heart += 1;
            PlayerPrefs.SetInt("Heart", MoneyManager.heart);
            Destroy(heart);
            heartRate = UnityEngine.Random.Range(heartRateMin, heartRateMax);
            timeAfterHeart = 0f;
        }
    }
    
}
//유니티는 왠만하면 스크립트엔 클래스 하나만 하자
