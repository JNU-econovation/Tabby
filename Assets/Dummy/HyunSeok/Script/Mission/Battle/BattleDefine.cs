using System;
using System.Collections;
using System.Collections.Generic;
using GlobalDefine;
using UnityEngine;

namespace Battle
{
    #region State
    public enum EManageState { IDLE, PROGRESS, PAUSE, GAMEOVER, END }
    public enum EAnimalState { IDLE, MOVE, DETECT_AUTO, DETECT_LOCKON, ATK, CC, DIE, END }
    public enum ECameraState { IDLE, FOLLOW, END }
    public enum EInputState { IDLE, DRAG, END }
    #endregion
    #region Interface
    #endregion
}
