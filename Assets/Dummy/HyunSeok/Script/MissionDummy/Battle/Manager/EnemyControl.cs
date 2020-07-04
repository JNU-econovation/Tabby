using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleDummy
{
    public class EnemyControl : MonoBehaviour
    {
        // 적들
        [SerializeField]
        private List<Enemy> enemies;
        public List<Enemy> Enemies { get => enemies; set => enemies = value; }

        private void Awake ()
        {
            BattleManager._instance.EnemyControl = this;
        }
    }
}
