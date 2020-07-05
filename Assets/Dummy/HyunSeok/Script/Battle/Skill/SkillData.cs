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
        [SerializeField]
        private int index;
        public int Index { get => index; }

        [SerializeField]
        private string skillName;

        [SerializeField]
        private Sprite icon;
        [Header("Value")]
        [SerializeField]
        private float coolTime;

        [SerializeField]
        private int skillPowerInt;

        [SerializeField]
        private float skillPowerPercent;
        [Header("Type")]
        [SerializeField]
        private BattleDefine.ESkillType type;

        public BattleDefine.ESkillType Type { get => type; set => type = value; }

        [SerializeField]
        private BattleDefine.ESkillTarget target;

        [SerializeField]
        private BattleDefine.ESkillCastingType castingType;
        public BattleDefine.ESkillCastingType CastingType
        {
            get { return castingType; }
            set { castingType = value; }
        }
        [Header("CC Info")]
        [SerializeField]
        private BattleDefine.ESkillCCType ccType;

        [SerializeField]
        private float ccTime;

        [SerializeField]
        private float ccPower;
        [Header("Link skill")]

        [SerializeField]
        private SkillData linkSkill;

        [SerializeField]
        public SkillData LinkSkill
        {
            get=> linkSkill;
            set
            {
                linkSkill = value;
            }
        }
    }
}

