using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class BattleEffect : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(false);
        }
        public void EndEffect()
        {
            gameObject.SetActive(false);
        }
    }
}
