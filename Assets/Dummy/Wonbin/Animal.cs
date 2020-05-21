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
    Rigidbody2D animalrigidbody;
    private Animator animator;
    public Sprite growUp;


    private SpriteRenderer spriteRenderer;

    private float speed = 4f;
    private int startTargetDistance=6;

    private float heartRateMin = 5f; //최소 생성주기
    private float heartRateMax = 7f; //최대 생성주기
    private float heartRate;
    public GameObject heartPrefabs;
    private float timeAfterHeart;

    GameObject heart;

    private Vector2Int bottomLeft, topRight;
    public List<Node> FinalNodeList;
    Vector2Int targetPos;
    public bool allowDiagonal, dontCrossCorner;
    private int i = 0;
    private int sizeX, sizeY;
    Node[,] NodeArray;
    Node StartNode, TargetNode, CurNode;
    List<Node> OpenList, ClosedList;


    float distance = 10;


    void Start()
    {
        bottomLeft.x = -22;
        bottomLeft.y = -11;
        topRight.x = 22;
        topRight.y = 10;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animalrigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pathFindingStart();
        heartRate = UnityEngine.Random.Range(heartRateMin, heartRateMax);

        
    }

    public void pathFindingStart()
    {
        //FinalNodeList 0부터 시작
        i = 0;
        nodeSetting();
        randomSetting();
        pathFinding();
    }

    private void Update()
    {
        if (timeAfterHeart <= heartRate)
            timeAfterHeart += Time.deltaTime;
        if (timeAfterHeart >= heartRate)
            makeHeart();

        //pathFinding이 끝난 길 Node 리스트를 따라 이동, 한칸 이동 후 i++
        animalrigidbody.position = Vector2.MoveTowards(animalrigidbody.position,new Vector2(FinalNodeList[i+1].x,FinalNodeList[i+1].y),speed*Time.deltaTime);
        animalanimation();

        if (animalrigidbody.position.x ==FinalNodeList[i + 1].x && animalrigidbody.position.y ==FinalNodeList[i + 1].y){
            i++;
        }


        if (Input.GetKeyDown(KeyCode.R))
            spriteRenderer.sprite = growUp;

        reFinding();


    }

    void reFinding()
    {
        if (i == (FinalNodeList.Count - 1))
        {
            i = 0;
            randomSetting();
            pathFinding();
        }
    }

    void makeHeart()
    {
            if (transform.childCount == 0)
                heart = (GameObject)Instantiate(heartPrefabs, gameObject.transform.position, gameObject.transform.rotation);
            heart.transform.parent = gameObject.transform;
            timeAfterHeart = 0f;
    }

    void animalanimation()//이동방향에 따른 스크립트 변경
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


        

    void randomSetting() 
    {
        //현재위치에서 최소 startTargetDistance만큼 떨어져 있고 장애물이 없는 지점을 targetPos로 지정
        targetPos = new Vector2Int(UnityEngine.Random.Range(bottomLeft.x, topRight.x), UnityEngine.Random.Range(bottomLeft.y, topRight.y));
        while ((NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y].isWall == true || Vector2Int.Distance(new Vector2Int((int)animalrigidbody.position.x, (int)animalrigidbody.position.y), targetPos)<startTargetDistance))
            targetPos = new Vector2Int(UnityEngine.Random.Range(bottomLeft.x, topRight.x), UnityEngine.Random.Range(bottomLeft.y, topRight.y));
    }
    void OnMouseDrag()
    {
        if (timeAfterHeart>=1)
            animalDrag();
    }

    void animalDrag()
    {
        animator.SetBool("tapAnimal", true); //뜬 애니메이션
        //드래그하면 들림. 커서를 따라 이동
        animalrigidbody.position = new Vector2(animalrigidbody.position.x, animalrigidbody.position.y + 3);
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,
        Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        animalrigidbody.position = objPosition;
        animator.SetBool("tapAnimal", true);
    }


    private void OnMouseUp()
    {
        //다시 랜덤지정으로 길찾기 시작
        randomSetting();
        pathFinding();
        i = 0;
        //애니메이션 돌아옴
        animator.SetBool("tapAnimal", false);
    }

    private void OnMouseDown()
    {
        if (transform.childCount != 0)
        {
            MoneyManager.heart += 1;
            PlayerPrefs.SetInt("Heart", MoneyManager.heart);
            Destroy(heart);
            heartRate = UnityEngine.Random.Range(heartRateMin, heartRateMax);
        }
    }

    public class Node
{
    public Node(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

    public bool isWall;
    public Node ParentNode;

    // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
    public int x, y, G, H;
    public int F { get { return G + H; } }
}


    public void nodeSetting()
    {
        // NodeArray의 크기 정해주고, x, y 대입 Layer가 Wall로 지정된 장애물 Collider가 있는 Node는 iswall=true
        sizeX = topRight.x - bottomLeft.x + 1;
        sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Node[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.4f))
                    if (col.gameObject.layer == LayerMask.NameToLayer("Wall")) isWall = true;

                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
            }
        }
    }

    void pathFinding() {
        //시작노드(현재위치), 타깃노드(targitPos) 설정
        StartNode = NodeArray[(int)(animalrigidbody.position.x - bottomLeft.x), (int)(animalrigidbody.position.y - bottomLeft.y)];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

        //길찾기 노드목록 생성
        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();


        while (OpenList.Count > 0)
        {
            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            CurNode = OpenList[0];
            for (int i3 = 1; i3 < OpenList.Count; i3++)
                if (OpenList[i3].F <= CurNode.F && OpenList[i3].H < CurNode.H) CurNode = OpenList[i3];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            // 최종 노드리스트 설정
            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();
            }


            // 대각선이동
            if (allowDiagonal)
            {
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            }

            // 수평수직이동
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
    }

    void OpenListAdd(int checkX, int checkY)
    {
        // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // 대각선 허용시, 벽 사이로 통과 안됨
            if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

            // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
            if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;


            // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }

    
    void OnDrawGizmos()
    {
            for (int i = 0; i < FinalNodeList.Count - 1; i++)
                Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
    }
    
}


