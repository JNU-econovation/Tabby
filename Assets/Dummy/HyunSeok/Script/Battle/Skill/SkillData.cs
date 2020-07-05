using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Battle
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "Data/Battle/SkillData")]
    public class SkillData : ScriptableObject
    {
        [SerializeField]
        private int index;

        [SerializeField]
        private string skillName;

        [SerializeField]
        private Sprite icon;

        [SerializeField]
        private float castingTime;
        public float CastingTime
        {
            get { return castingTime; }
            set { castingTime = value; }
        }

        [SerializeField]
        private int skillPowerInt;

        [SerializeField]
        private float skillPowerPercent;

        [SerializeField]
        private BattleDefine.ESkillType type;

        [SerializeField]
        private BattleDefine.ESkillTarget target;

        [SerializeField]
        private BattleDefine.ESkillCastingType castingType;
        public BattleDefine.ESkillCastingType CastingType
        {
            get { return castingType; }
            set { castingType = value; }
        }

        [SerializeField]
        private BattleDefine.ESkillCCType ccType;

        [SerializeField]
        private float ccTime;

        [SerializeField]
        private float ccPower;

        [SerializeField]
        private SkillData linkSkill;
    }
}

