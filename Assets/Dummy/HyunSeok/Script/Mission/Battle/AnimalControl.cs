using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class AnimalControl : MonoBehaviour
    {
        // 이벤트
        public delegate void Event ();
        public event Event EvTargetAnimalNull;
        public event Event EvTargetAnimalChange;
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
        
        private void Awake() {
            BattleManager._instance.AnimalControl = this;
        }
    }
}
