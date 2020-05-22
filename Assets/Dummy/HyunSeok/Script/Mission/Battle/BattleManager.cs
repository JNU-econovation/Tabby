using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class BattleManager : MonoBehaviour
    {
        // 싱글톤
        public static BattleManager _instance;

        #region Reference
        // Battle 씬 카메라
        private CameraControl cameraControl;
        public CameraControl CameraControl { get => cameraControl; set => cameraControl = value; }
        // Command
        private Command command;
        public Command Command { get => command; set => command = value; }
        // Input Manager
        private InputManager inputManager;
        public InputManager InputManager { get => inputManager; set => inputManager = value; }
        // Animal Contorl
        private AnimalControl animalControl;
        public AnimalControl AnimalControl { get => animalControl; set => animalControl = value; }
        // Enemy Contorl
        private EnemyControl enemyControl;
        public EnemyControl EnemyControl { get => enemyControl; set => enemyControl = value; }
        // Bviual Control
        private BVisualControl bVisualControl;
        public BVisualControl BVisualControl { get => bVisualControl; set => bVisualControl = value; }
        #endregion

        // Battle State
        private EManageState manageState;
        public EManageState ManageState
        {
            get => manageState;
            set => manageState = value;
        }

        void Awake ()
        {
            if (_instance == null)
                _instance = this;
        }

        void Start () { }

        void Update ()
        {
            if (Input.GetKeyDown (KeyCode.F1))
            {

            }
            if (Input.GetKeyDown (KeyCode.F2))
            {

            }
        }

    }
}
