using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieTabby : MonoBehaviour, IAnimalAct
{
    void IAnimalAct.Idle(){

    }

    void IAnimalAct.Move(){

    }

    void Awake()
    {
        ((IAnimalAct)this).Idle();
    }
}
