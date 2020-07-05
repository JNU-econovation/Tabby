using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public interface IState
    {
        bool IsDeny(BattleDefine.EBattlerState state);
        void OnEnter();
        void Run();
        void OnExit();
    }
}

