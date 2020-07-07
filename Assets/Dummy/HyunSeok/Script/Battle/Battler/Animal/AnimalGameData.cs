using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;

namespace GameData
{
    [CreateAssetMenu (fileName = "AnimalGameData", menuName = "Data/Animal GameData")]
    public class AnimalGameData : ScriptableObject
    {

        [SerializeField]
        private int index;
        public int Index { get => index; set => index = value; }

        [SerializeField]
        private string code;
        public string Code { get => code; set => code = value; }

        [SerializeField]
        private string animalName;
        public string AnimalName { get => animalName; set => animalName = value; }

        [SerializeField]
        private int exp;
        public int Exp { get => exp; set => exp = value; }

        [SerializeField]
        private float maxHP;
        public float MapHP { get => maxHP; set => maxHP = value; }

        public BattleDefine.VoidEventInt EvHP;
        [SerializeField]
        private float hp;
        public float HP { get => hp;
            set
            {
                hp = value;
                EvHP?.Invoke(battleIndex);
            }

        }

        [SerializeField]
        private float atk;
        public float Atk { get => atk; set => atk = value; }

        [SerializeField]
        private float atkSpd;
        public float AtkSpd { get => atkSpd; set => atkSpd = value; }

        [SerializeField]
        private float tough;
        public float Tough { get => tough; set => tough = value; }

        [SerializeField]
        private float focus;
        public float Focus { get => focus; set => focus = value; }

        [SerializeField]
        private float critical;
        public float Critical { get => critical; set => critical = value; }

        [SerializeField]
        private int battleIndex;
        public int BattleIndex { get => battleIndex; set => battleIndex = value; }
    }
}
