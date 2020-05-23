using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class AtkColPool : MonoBehaviour
    {
        public const int initPoolSize = 5;
        // 키 값
        
        public AttackCollision attackCollision;
        public Queue<AttackCollision> collisions;

        public void CreatePool (AttackCollision atkCol)
        {
            attackCollision = atkCol;
            // 원, 근거리 여부에 따라 pool 초기 사이즈 지정
            int poolSize =
                atkCol.AttackType == EAttackType.MELEE ? 1 : initPoolSize;
            for (int i = 0; i < poolSize; i++)
            {
                collisions.Enqueue (Instantiate (atkCol) as AttackCollision);
            }
        }
        public void Shot ()
        {
            Shot
        }
        public void IncrasePool()
        {

        }
    }
}
