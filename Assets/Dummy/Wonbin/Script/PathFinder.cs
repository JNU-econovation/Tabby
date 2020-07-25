using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder 
{
    [SerializeField]
    private Vector2Int bottomLeft =new Vector2Int(-22, -11), topRight=new Vector2Int(22, 5);
    public List<Node> FinalNodeList;
    public int FinalListNodeNumber;
    Vector2Int targetPos;
    public bool allowDiagonal, dontCrossCorner;
    private int sizeX, sizeY;
    Node[,] NodeArray;
    Node StartNode, TargetNode, CurNode;
    List<Node> OpenList, ClosedList;


    public int GetBottomLeftX()
    {
        return bottomLeft.x;
    }
    public int GetBottomLeftY()
    {
        return bottomLeft.y;
    }
    public int GetTopRightX()
    {
        return topRight.x;
    }
    public int GetTopRightY()
    {
        return topRight.y;
    }
    public void PathFindingStart(Rigidbody2D rigidbody2D, int startTargetDistance)
    {
        //FinalNodeList 0부터 시작
        FinalListNodeNumber = 0;
        NodeSetting();
        RandomSetting(rigidbody2D, startTargetDistance);
        PathFinding(rigidbody2D);
    }

    public void ReFinding(Rigidbody2D rigidbody2D, int startTargetDistance)
    {
        FinalListNodeNumber = 0;
        RandomSetting(rigidbody2D, startTargetDistance);
        PathFinding(rigidbody2D);
    }

    public void DropAnimal(Rigidbody2D rigidbody2D, int startTargetDistance)
    {
        FinalListNodeNumber = 0;
        RandomSetting(rigidbody2D, startTargetDistance);
        PathFinding(rigidbody2D);
    }

    public void FollwingPath(Rigidbody2D rigidbody2D, float speed)
    {
        if(FinalNodeList.Count!=0)
            rigidbody2D.position = Vector2.MoveTowards(rigidbody2D.position, new Vector2(FinalNodeList[FinalListNodeNumber + 1].x, FinalNodeList[FinalListNodeNumber + 1].y), speed * Time.deltaTime);
        //Animalanimation();

        if (FinalNodeList.Count != 0&&rigidbody2D.position.x == FinalNodeList[FinalListNodeNumber + 1].x && rigidbody2D.position.y == FinalNodeList[FinalListNodeNumber + 1].y)
        {

            FinalListNodeNumber++;
        }

        if (FinalNodeList.Count == 0||FinalListNodeNumber==FinalNodeList.Count-1)
            ReFinding(rigidbody2D, 6);
    }

    //목적지 랜덤 지정
    public void RandomSetting(Rigidbody2D rigidbody2D, int startTargetDistance)
    {
        //현재위치에서 최소 startTargetDistance만큼 떨어져 있고 장애물이 없는 지점을 targetPos로 지정
        targetPos = new Vector2Int(UnityEngine.Random.Range(bottomLeft.x, topRight.x), UnityEngine.Random.Range(bottomLeft.y, topRight.y));
        while ((NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y].isWall == true || Vector2Int.Distance(new Vector2Int((int)rigidbody2D.position.x, (int)rigidbody2D.position.y), targetPos) < startTargetDistance))
            targetPos = new Vector2Int(UnityEngine.Random.Range(bottomLeft.x, topRight.x), UnityEngine.Random.Range(bottomLeft.y, topRight.y));
        
    }

    public Vector2 RandomSpawnSetting()
    {
        //현재위치에서 최소 startTargetDistance만큼 떨어져 있고 장애물이 없는 지점을 targetPos로 지정
        Vector2 spawnPos = new Vector2Int(UnityEngine.Random.Range(bottomLeft.x, topRight.x), UnityEngine.Random.Range(bottomLeft.y, topRight.y));
        while ((NodeArray[(int)spawnPos.x - bottomLeft.x, (int)spawnPos.y - bottomLeft.y].isWall == true))
            spawnPos = new Vector2Int(UnityEngine.Random.Range(bottomLeft.x, topRight.x), UnityEngine.Random.Range(bottomLeft.y, topRight.y));
        return spawnPos;

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

    public void NodeSetting()
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
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 1f))
                    if (col.gameObject.layer == LayerMask.NameToLayer("Wall")) isWall = true;

                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
            }
        }
    }

    public void PathFinding(Rigidbody2D rigidbody2D)
    {
        //시작노드(현재위치), 타깃노드(targitPos) 설정
        StartNode = NodeArray[(int)(rigidbody2D.position.x - bottomLeft.x), (int)(rigidbody2D.position.y - bottomLeft.y)];
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

    void AnimalAnimation(Node[] FinalNodeList, Rigidbody2D rigidbody2D, Animator animator, int FinalListNodeNumber)//이동방향에 따른 스크립트 변경
    {
        if ((FinalNodeList[FinalListNodeNumber + 1].x - rigidbody2D.position.x) == 0 && (FinalNodeList[FinalListNodeNumber + 1].y - rigidbody2D.position.y) < 0)
            animator.SetInteger("rotate", 0);
        else if ((FinalNodeList[FinalListNodeNumber + 1].x - rigidbody2D.position.x) < 0 && (FinalNodeList[FinalListNodeNumber + 1].y - rigidbody2D.position.y) > 0)
            animator.SetInteger("rotate", 1);
        else if ((FinalNodeList[FinalListNodeNumber + 1].x - rigidbody2D.position.x) < 0 && (FinalNodeList[FinalListNodeNumber + 1].y - rigidbody2D.position.y) == 0)
            animator.SetInteger("rotate", 1);
        else if ((FinalNodeList[FinalListNodeNumber + 1].x - rigidbody2D.position.x) < 0 && (FinalNodeList[FinalListNodeNumber + 1].y - rigidbody2D.position.y) < 0)
            animator.SetInteger("rotate", 1);
        else if ((FinalNodeList[FinalListNodeNumber + 1].x - rigidbody2D.position.x) == 0 && (FinalNodeList[FinalListNodeNumber + 1].y - rigidbody2D.position.y) > 0)
            animator.SetInteger("rotate", 2);
        else if ((FinalNodeList[FinalListNodeNumber + 1].x - rigidbody2D.position.x) > 0 && (FinalNodeList[FinalListNodeNumber + 1].y - rigidbody2D.position.y) > 0)
            animator.SetInteger("rotate", 3);
        else if ((FinalNodeList[FinalListNodeNumber + 1].x - rigidbody2D.position.x) > 0 && (FinalNodeList[FinalListNodeNumber + 1].y - rigidbody2D.position.y) == 0)
            animator.SetInteger("rotate", 3);
        else if ((FinalNodeList[FinalListNodeNumber + 1].x - rigidbody2D.position.x) > 0 && (FinalNodeList[FinalListNodeNumber + 1].y - rigidbody2D.position.y) < 0)
            animator.SetInteger("rotate", 3);
    }

}
