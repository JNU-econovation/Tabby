using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleDummy
{
    public class BVisualControl : MonoBehaviour
    {
        private void Awake ()
        {
            BattleManager._instance.BVisualControl = this;

        }
        private void Start ()
        {
            InitEvent ();
        }
        void InitEvent ()
        {
            // InputManager visual control
            BattleManager._instance.InputManager.EvDragBegin +=
                new InputManager.Event (OnAnimalMoveArrow);
            BattleManager._instance.InputManager.EvDragging +=
                new InputManager.Event (VisualizeAnimalMoveArrow);
            BattleManager._instance.InputManager.EvDragEnd +=
                new InputManager.Event (OffAnimalMoveArrow);
            // Animal Control visual control
            BattleManager._instance.AnimalControl.EvAfterTargetAnimalChange +=
                new AnimalControl.EventAnimal (OnAnimalSelectMark);
            BattleManager._instance.AnimalControl.EvBeforeTargetAnimalChange +=
                new AnimalControl.Event (OffAnimalSelectMark);
            BattleManager._instance.AnimalControl.EvBeforeTargetAnimalChange +=
                new AnimalControl.Event (OffAnimalMoveArrow);
        }
        public void OnAnimalMoveArrow ()
        {
            if (BattleManager._instance.AnimalControl.TargetAnimal == null)
                return;
            BattleManager._instance.AnimalControl.TargetAnimal.BattleVisual.OnMoveArrow ();
        }
        public void VisualizeAnimalMoveArrow ()
        {
            if (BattleManager._instance.AnimalControl.TargetAnimal == null)
                return;
            Vector3 dir =
                (BattleManager._instance.InputManager.EndPoint - BattleManager._instance.InputManager.BeginPoint).normalized;
            float dist =
                Vector2.Distance (BattleManager._instance.InputManager.BeginPoint, BattleManager._instance.InputManager.EndPoint);
            dist = Mathf.Clamp (dist, 0f, 0.5f * BattleManager._instance.CameraControl.cameraHeight);
            BattleManager._instance.AnimalControl.TargetAnimal.BattleVisual.VisualizeMoveArrow (dir, dist);
        }

        public void OffAnimalMoveArrow ()
        {
            if (BattleManager._instance.AnimalControl.TargetAnimal == null)
                return;
            BattleManager._instance.AnimalControl.TargetAnimal.BattleVisual.OffMoveArrow ();
        }
        public void OnAnimalSelectMark (Animal target)
        {
            if (target == null)
                return;
            target.BattleVisual.OnSelectMark ();
        }
        public void OffAnimalSelectMark ()
        {
            if (BattleManager._instance.AnimalControl.TargetAnimal == null)
                return;
            BattleManager._instance.AnimalControl.TargetAnimal.BattleVisual.OffSelectMark ();
        }
    }
}
