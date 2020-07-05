using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

namespace Battle
{
    public interface IBattlerAdapter
    {
        void Damaged(SkillData skillData, float damage);
    }
}

