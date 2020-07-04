using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleDummy
{
    public class Command : MonoBehaviour
    {
        private void Awake ()
        {
            BattleManager._instance.Command = this;
        }

        void Start ()
        {
            InitEvent ();
        }
        void InitEvent ()
        {
            BattleManager._instance.InputManager.EvDragEnd += new InputManager.Event (CmdMove);
            BattleManager._instance.InputManager.EvClickEnemy += new InputManager.EventEnemy (CmdLockOn);
        }
        /**
        *   현재 타겟 된 유닛에게 움직임 명령을 내린다
        */
        void CmdMove ()
        {
            if (BattleManager._instance.AnimalControl.TargetAnimal == null)
                return ;
            Vector3 dir = (BattleManager._instance.InputManager.EndPoint - BattleManager._instance.InputManager.BeginPoint).normalized;
            float dist = Vector2.Distance (BattleManager._instance.InputManager.BeginPoint, BattleManager._instance.InputManager.EndPoint);
            dist = Mathf.Clamp (dist, 0f, 0.5f * BattleManager._instance.CameraControl.cameraHeight);
            BattleManager._instance.AnimalControl.TargetAnimal.CmdMove (dir, dist);
        }
        /**
        *   현재 타겟 된 적을 락온하는 명령을 모든 동물에게 내린다
        */
        void CmdLockOn (Enemy enemy)
        {
            if (enemy == null)
                return;
            BattleManager._instance.AnimalControl.CmdLockOn (enemy);
        }

    }
}
