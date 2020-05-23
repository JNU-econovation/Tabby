using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class AtkColPoolControl : MonoBehaviour
    {
        private int poolSize;
        public int PoolSize { get; set; }
        private List<AtkColPool> atkPools;

        [SerializeField]
        private AtkColPool pool;

        public void InitPool (List<AttackCollision> atkCols)
        {
            for (int i = 0; i < atkCols.Count; i++)
            {
                AtkColPool newPool = Instantiate (pool) as AtkColPool;
                newPool.CreatePool (atkCols[i]);
                atkPools.Add (newPool);
            }
        }

        /*public void Shot(int idx)
        {
            
        }*/
    }
}
