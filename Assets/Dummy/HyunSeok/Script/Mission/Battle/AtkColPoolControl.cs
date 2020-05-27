using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class AtkColPoolControl : MonoBehaviour
    {
        private int poolSize;
        public int PoolSize { get; set; }
        // pool 프리팹들 손으로 ㅋㅋㅋ 채워줘야 함
        [SerializeField]
        private List<AtkColPool> atkColPools;
        private void Awake ()
        {
            InitPool ();
        }
        public void InitPool ()
        {
            /*foreach (AtkColPool pool in atkColPools)
            {
                AtkColPool newPool = Instantiate(pool) as AtkColPool;
                newPool.transform.parent = this.transform;
            }*/
        }

        public void Shot(int index, Enemy target)
        {
            atkColPools[index].GetAtkCol ().Shot(target);
        }

        void Update()
        {
            if (Input.GetKeyDown (KeyCode.F1))
            {
                atkColPools[0].GetAtkCol ().Shot(BattleManager._instance.EnemyControl.Enemies[0]);
            }
            if (Input.GetKeyDown (KeyCode.F2))
            {

            }
        }
    }
}
