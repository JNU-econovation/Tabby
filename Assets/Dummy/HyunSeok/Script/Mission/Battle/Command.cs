using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class Command : MonoBehaviour
    {
        private void Awake ()
        {
            BattleManager._instance.Command = this;
        }

        void Start()
        {
            Init();
        }

        void CmdMove(Animal target)
        {
            if (target == null)
                return ;
            Vector3 dir = (BattleManager._instance.InputManager.endPoint - BattleManager._instance.InputManager.beginPoint).normalized;
            float dist = Vector2.Distance(BattleManager._instance.InputManager.beginPoint, BattleManager._instance.InputManager.endPoint);
            dist = Mathf.Clamp(dist, 0f, 0.5f * BattleManager._instance.CameraControl.cameraHeight);
            target.CmdMove(dir, dist);
        }

        void Init ()
        {
            BattleManager._instance.InputManager.EvAnimalDragEnd += new InputManager.EventAnimal(CmdMove);
            BattleManager._instance.InputManager.EvAnimalDragEnd += new InputManager.EventAnimal(CmdMove);
        }

    }
}
