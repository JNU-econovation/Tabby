using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class AttackCollision : MonoBehaviour
    {
        [SerializeField]
        private EAtkColPoolSize poolSize;
        public EAtkColPoolSize PoolSize { get => poolSize; set => poolSize = value; }
        [SerializeField]
        private EAtkColDestroyType destroyType;
        public EAtkColDestroyType DestroyType { get => destroyType; set => destroyType = value; }
        private float damage;
        public float Damage { get => damage; set => damage = value; }
        // 파괴 시 돌아가야 할 풀
        private AtkColPool atkColPool;

        public void Init(AtkColPool atkColPool)
        {
            gameObject.SetActive(false);
            this.atkColPool = atkColPool;
        }
        public void Shot (Vector3 dir, float speed)
        {
            gameObject.SetActive(true);
            // 공격
        }
        // 파괴 시 pool로 돌아가야함
        public void Destroyed()
        {
            gameObject.SetActive(false);
            atkColPool.ReturnToPool(this);
        }
    }
}
