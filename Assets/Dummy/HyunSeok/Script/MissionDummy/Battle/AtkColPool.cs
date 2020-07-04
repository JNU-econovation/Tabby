using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleDummy
{
    public class AtkColPool : MonoBehaviour
    {
        public const int initPoolSize = 5;
        // 키 값

        // 소환할 atkCol 프리팹
        [SerializeField]
        private AttackCollision atkCol;
        private Queue<AttackCollision> atkCols;

        private void Awake ()
        {
            InitPool ();
        }
        public void InitPool ()
        {
            // 최초의 atkCol 초기화
            atkCols = new Queue<AttackCollision> ();
            atkCol.Init (this);
            atkCols.Enqueue (atkCol);

            // 원, 근거리 여부에 따라 추가 atkCol 생성
            int poolSize = (int) atkCol.PoolSize;
            for (int i = 0; i < poolSize; i++)
            {
                AttackCollision col = Instantiate (atkCol) as AttackCollision;
                col.transform.parent = this.transform;
                col.Init (this);
                atkCols.Enqueue (col);
            }
            Debug.Log (atkCols.Count);
        }
        // 공격 시 호출
        public AttackCollision GetAtkCol ()
        {
            // 만약 pool에 남아 있는 것이 없을 경우
            if (atkCols.Count == 0)
            {
                // 새롭게 생성
                AttackCollision col = Instantiate (atkCol) as AttackCollision;
                col.Init (this);
                // 부모에게 분리
                col.transform.parent = null;
                return (col);
            }
            else
            {
                // 기존 있는 것 반환
                AttackCollision col = atkCols.Dequeue ();
                // 부모에게 분리
                col.transform.parent = null;
                return col;
            }
        }
        // 공격 종료 시
        public void ReturnToPool (AttackCollision atkCol)
        {
            // 다시 부모 돌려주기
            atkCol.transform.parent = this.transform;
            atkCols.Enqueue (atkCol);
        }
    }
}
