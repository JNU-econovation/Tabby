using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Battle
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "Data/Battle/SkillData")]
    public class SkillData : ScriptableObject
    {
        [Header("Asset index")]
        public int index;
        public string skillName;
        public Sprite icon;
        [Header("Value")]
        public float coolTime;
        public int skillPowerInt;
        public float skillPowerPercent;
        [Header("Type")]
        public BattleDefine.ESkillType type;
        public BattleDefine.ESkillTarget target;
        public BattleDefine.ESkillCastingType castingType;
        public BattleDefine.ESkillStatType statType;
        [Header("CC Info")]
        public BattleDefine.ESkillCCType ccType;
        public float ccTime;
        public float ccPower;
        [Header("For enemy")]
        public float patternPercent;
    }
}

