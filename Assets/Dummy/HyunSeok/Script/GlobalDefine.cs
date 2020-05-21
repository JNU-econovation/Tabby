using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalDefine
{
    #region Interface
    public interface IState
    {
        IEnumerator OnEnter ();
        IEnumerator Progress ();
        IEnumerator OnExit ();
    }
    #endregion
}
