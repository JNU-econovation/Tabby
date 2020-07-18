using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class BattleDefine
    {
        public enum EBattleState { Ready, Playing, Pause, BattleOver, End }
        public enum EBattlerState { Ready, Idle, Skill, Stun, Damaged, Down, Win }
        public enum ECCState { None, Stun, StatDown_Percent, StatDown_int, End }

        public enum ESkillType { Attack, Skill, Special, End }
        public enum ESkillTarget { Enemy, Me, TeamAll, TeamExceptMe, TeamBackAll, TeamBackOne, TeamFrontAll, TeamFrontOne, FrontAll, One, Two, Three, All, End }
        public enum ESkillCastingType { Cancled, NonCancled, End }
        public enum ESkillCCType { None, Stun, StatDown_Percent, StatDown_int, End }
        public enum ESkillStatType { None, Atk, AtkSpd, Tough, Focus, Critical }

        public delegate void VoidEvent();
    }
}

