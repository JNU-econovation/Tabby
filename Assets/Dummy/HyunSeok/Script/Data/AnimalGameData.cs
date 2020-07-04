using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    [CreateAssetMenu (fileName = "AnimalGameData", menuName = "Data/Animal GameData")]
    public class AnimalGameData : ScriptableObject
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
        private float tough;
        public float Tough { get => tough; set => tough = value; }
    }
}
