using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu (fileName = "Stat Data", menuName = "Data/Battle Data/Animal Data/Stat Data")]
    public class AnimalStatData : ScriptableObject
    {
        [SerializeField]
        private float hp;
        public float HP { get => hp; set => hp = value; }

        [SerializeField]
        private float atk;
        public float Atk { get => atk; set => hp = atk; }

        [SerializeField]
        private float atkSpd;
        public float AtkSpd { get => atkSpd; set => hp = atkSpd; }
    }
}
