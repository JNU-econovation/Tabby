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
    protected SpriteRenderer spriteRenderer;
    protected Sprite babyAnimalSprite;
    protected Sprite growUpSprite;

    PathFinder pathfinder=new PathFinder();

    Rigidbody2D animalrigidbody;
    private Animator animator;

    protected int animalCount;

    private float speed = 4f;

    private float heartRateMin = 5f; //최소 생성주기
    private float heartRateMax = 7f; //최대 생성주기
    private float heartRate;

    [SerializeField]
    private GameObject heartPrefabs;
    private float timeAfterHeart;

    GameObject heart;


    float distance = 10;


    void Start()
    {
        
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animalrigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pathfinder.PathFindingStart(animalrigidbody, 6);
        heartRate = UnityEngine.Random.Range(heartRateMin, heartRateMax);

        
    }

    

    private void Update()
    {
        //하트가 없어진 이후로 흐른 시간 체크
        if (timeAfterHeart <= heartRate)
            timeAfterHeart += Time.deltaTime;
        //랜덤시간 이상이 됐을때 하트 생성
        if (timeAfterHeart >= heartRate)
        {
            if(transform.childCount == 0)
                MakeHeart();
        }


        //pathFinding이 끝난 길 Node 리스트를 따라 이동, 한칸 이동 후 i++
        pathfinder.FollwingPath(animalrigidbody, 4);


        if (Input.GetKeyDown(KeyCode.R))
            spriteRenderer.sprite = growUpSprite;
        //목적지에 도달했다면 다시 길찾기
        pathfinder.ReFinding(animalrigidbody, 6);


    }



    //하트생성
    void MakeHeart()
    {
        if (transform.childCount == 0)
            heart = (GameObject)Instantiate(heartPrefabs, gameObject.transform.position, gameObject.transform.rotation);
        heart.transform.parent = gameObject.transform;
    }

    void Animalanimation()//이동방향에 따른 스크립트 변경
    {
        if ((FinalNodeList[i + 1].x - animalrigidbody.position.x) == 0 && (FinalNodeList[i + 1].y - animalrigidbody.position.y) < 0)
            animator.SetInteger("rotate", 0);
        else if ((FinalNodeList[i + 1].x - animalrigidbody.position.x) < 0 && (FinalNodeList[i + 1].y - animalrigidbody.position.y) > 0)
            animator.SetInteger("rotate", 1);
        else if ((FinalNodeList[i + 1].x - animalrigidbody.position.x) < 0 && (FinalNodeList[i + 1].y - animalrigidbody.position.y) == 0)
            animator.SetInteger("rotate", 1);
        else if ((FinalNodeList[i + 1].x - animalrigidbody.position.x) < 0 && (FinalNodeList[i + 1].y - animalrigidbody.position.y) < 0)
            animator.SetInteger("rotate", 1);
        else if ((FinalNodeList[i + 1].x - animalrigidbody.position.x) == 0 && (FinalNodeList[i + 1].y - animalrigidbody.position.y) > 0)
            animator.SetInteger("rotate", 2);
        else if ((FinalNodeList[i + 1].x - animalrigidbody.position.x) > 0 && (FinalNodeList[i + 1].y - animalrigidbody.position.y) > 0)
            animator.SetInteger("rotate", 3);
        else if ((FinalNodeList[i + 1].x - animalrigidbody.position.x) > 0 && (FinalNodeList[i + 1].y - animalrigidbody.position.y) == 0)
            animator.SetInteger("rotate", 3);
        else if ((FinalNodeList[i + 1].x - animalrigidbody.position.x) > 0 && (FinalNodeList[i + 1].y - animalrigidbody.position.y) < 0)
            animator.SetInteger("rotate", 3);
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
