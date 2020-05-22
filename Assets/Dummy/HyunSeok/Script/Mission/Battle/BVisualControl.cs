using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
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
            BattleManager._instance.InputManager.EvAnimalDragBegin +=
                new InputManager.EventAnimal (OnAnimalMoveArrow);
            BattleManager._instance.InputManager.EvAnimalDragging +=
                new InputManager.EventAnimal (VisualizeAnimalMoveArrow);
            BattleManager._instance.InputManager.EvAnimalDragEnd +=
                new InputManager.EventAnimal (OffAnimalMoveArrow);
        }
        public void OnAnimalMoveArrow (Animal target)
        {
            if (BattleManager._instance.AnimalControl.TargetAnimal == null)
                return;
            target.BattleVisual.OnMoveArrow ();
        }
        public void VisualizeAnimalMoveArrow (Animal target)
        {
            if (BattleManager._instance.AnimalControl.TargetAnimal == null)
                return;
            Vector3 dir =
                (BattleManager._instance.InputManager.endPoint - BattleManager._instance.InputManager.beginPoint).normalized;
            float dist =
                Vector2.Distance (BattleManager._instance.InputManager.beginPoint, BattleManager._instance.InputManager.endPoint);
            dist = Mathf.Clamp (dist, 0f, 0.5f * BattleManager._instance.CameraControl.cameraHeight);
            target.BattleVisual.VisualizeMoveArrow (dir, dist);
        }

        public void OffAnimalMoveArrow (Animal target)
        {
            if (BattleManager._instance.AnimalControl.TargetAnimal == null)
                return;
            target.BattleVisual.OffMoveArrow ();
        }
    }
}
