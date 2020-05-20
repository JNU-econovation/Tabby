using System;
using System.Collections;
using System.Collections.Generic;
using GlobalDefine;
using UnityEngine;

namespace Battle
{
    abstract public class FSM<T>
    {
        private readonly T onwer;
    }
    #region State
    public enum EManageState { IDLE, PROGRESS, PAUSE, GAMEOVER, END }
    public enum EAnimalState { IDLE, MOVE, ATTACK, CC, DIE, END }
    #endregion
    #region Interface
    #endregion
}
