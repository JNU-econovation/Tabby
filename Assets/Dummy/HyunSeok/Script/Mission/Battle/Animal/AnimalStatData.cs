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
        public float Atk { get => atk; set => atk = value; }

        [SerializeField]
        private float atkSpd;
        public float AtkSpd { get => atkSpd; set => atkSpd = value; }

        [SerializeField]
        private float atkRange;
        public float AtkRange { get => atkRange; set => atkRange = value; }
    }
}
