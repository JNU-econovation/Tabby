using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

namespace Battle
{
    public class RedTurtle : Animal
    {
        Coroutine stateCrtn;
        void Start ()
        {
            InitFSM ();
        }

        void Update ()
        {
            if (Input.GetKeyDown (KeyCode.W))
            {
                stateControl.SetState (states[(int) EAnimalState.MOVE]);
            }

            stateControl.Run ();
        }
        protected override void InitFSM ()
        {
            states[(int) EAnimalState.IDLE] = new IdleState (this);
            states[(int) EAnimalState.MOVE] = new MoveState (this);
            stateControl = new HeadMachine<Animal> (states[(int) EAnimalState.IDLE]);
        }

        class IdleState : IState
        {
            private RedTurtle owner;
            public IdleState (RedTurtle owner) => this.owner = owner;
            public void OnEnter ()
            {
                Debug.Log ("Enter IdleState");
            }

            public void OnExit ()
            {
                Debug.Log ("Exit IdleState");
            }

            public void Run ()
            {
                Debug.Log ("Progress IdleState");
            }
        }

        class MoveState : IState
        {
            float time;
            private RedTurtle owner;
            public MoveState (RedTurtle owner) => this.owner = owner;

            public void OnEnter ()
            {
                time = 0f;
                Debug.Log ("Enter WalkState");
            }

            public void OnExit ()
            {
                Debug.Log ("Exit WalkState");
            }

            public void Run ()
            {
                time += Time.deltaTime;
                Debug.Log ("Progress WalkState");
                if (time > 2.0f)
                {
                    OnExit ();
                    owner.stateControl.Revert ();
                }

            }
        }
    }
}
