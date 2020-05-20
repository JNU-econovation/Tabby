using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager _instance;

        private EManageState manageState;
        public EManageState ManageState
        {
            get => manageState;
            set => manageState = value;
        }

        private List<Animal> animals;
    }
}
