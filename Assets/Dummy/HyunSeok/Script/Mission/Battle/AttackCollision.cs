using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class AttackCollision : MonoBehaviour
    {
        private EAtkColPoolSize poolSize;
        public EAtkColPoolSize PoolSize { get => poolSize; set => poolSize = value; }
        private EAtkColDestroyType destroyType;
        public EAtkColDestroyType DestroyType { get => destroyType; set => destroyType = value; }
        private float damage;
        public float Damage { get => damage; set => damage = value; }

        public void Shot (Vector3 dir, float speed)
        {
            // 공격
        }
    }
}
