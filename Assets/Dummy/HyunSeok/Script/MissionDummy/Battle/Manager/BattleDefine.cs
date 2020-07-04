using System;
using System.Collections;
using System.Collections.Generic;
using GlobalDefine;
using UnityEngine;

namespace BattleDummy
{
    #region State
    public enum EManageState { IDLE, PROGRESS, PAUSE, GAMEOVER, END }
    public enum EAnimalState { IDLE, MOVE, DETECT_AUTO, DETECT_LOCKON, ATK, CC, DIE, END }
    public enum ECameraState { IDLE, FOLLOW, END }
    public enum EInputState { IDLE, DRAG, END }
    public enum EAtkColState { IDLE, ACT, DIE, END}
    #endregion
    // 투사체 타입 모음
    #region AtkCol
    // 요구하는 pool의 사이즈
    public enum EAtkColPoolSize { SMALL = 1, BIG = 5 }
    /**
    *   오브젝트 충돌 시 유형
    *   TARGET : 타겟한테 반드시 적중한다
    *   SOFT : 즉시 사라진다   
    *   SOLID : 끝까지 갈 길 간다
    */
    public enum EAtkColDestroyType { TARGET, SOFT, SOLID }
    public enum EAtkColSprType { FIX, DIR }
    #endregion
}
