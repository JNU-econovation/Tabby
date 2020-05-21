using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class BattleManager : MonoBehaviour
    {
        // 싱글톤
        public static BattleManager _instance;
        // 이벤트
        public delegate void Event ();
        public event Event EvTargetAnimalNull;
        public event Event EvTargetAnimalChange;
        #region Reference
        // Battle 씬 카메라
        [SerializeField]
        private CameraControl cameraControl;
        public CameraControl CameraControl { get => cameraControl; set => cameraControl = value; }
        // Command
        [SerializeField]
        private Command command;
        public Command Command { get => command; set => command = value; }
        // Input Manager
        [SerializeField]
        private InputManager inputManager;
        public InputManager InputManager { get => inputManager; set => inputManager = value; }
        #endregion
        // Battle State
        private EManageState manageState;
        public EManageState ManageState
        {
            get => manageState;
            set => manageState = value;
        }
        // 현재 지정한 동물
        private Animal targetAnimal;
        public Animal TargetAnimal
        {
            get => targetAnimal;
            set
            {
                if (value == null)
                {
                    targetAnimal = value;
                    EvTargetAnimalNull ();
                }
                else
                {
                    if (!value.Equals (targetAnimal))
                    {
                        targetAnimal = value;
                        EvTargetAnimalChange ();
                    }
                }
            }
        }
        // 전체 동물
        [SerializeField]
        private List<Animal> animals;
        public List<Animal> Animals { get => animals; set => animals = value; }
        // 적들
        [SerializeField]
        private List<Enemy> enemies;
        public List<Enemy> Enemies { get => enemies; set => enemies = value; }
        private List<AnimalStatData> animalStats;

        void Awake ()
        {
            if (_instance == null)
                _instance = this;
            targetAnimal = null;
            /*for(int i = 0; i < animals.Count; i++)
            {
                animals[i].InitStat(animalStats[i]);
            }*/
        }

        void Start()
        {
            TargetAnimal = Animals[0];    
        }

        void Update ()
        {
            if (Input.GetKeyDown (KeyCode.F1))
            {
                TargetAnimal = null;
            }
            if (Input.GetKeyDown (KeyCode.F2))
            {
                TargetAnimal = Animals[0];
            }
        }
    }
}
