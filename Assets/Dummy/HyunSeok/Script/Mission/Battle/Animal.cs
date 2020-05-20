using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

namespace Battle
{
    public abstract class Animal : MonoBehaviour
    {
        protected HeadMachine<Animal> stateControl;
        protected IState[] states = new IState[(int)EAnimalState.END];
        protected abstract void InitFSM();
    }
}
