using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class BattleVisual : MonoBehaviour
    {
        [SerializeField]
        private Transform arrowBodyTransform;
        [SerializeField]
        private Transform arrowEdgeTransform;
        [SerializeField]
        private GameObject selectMark;

        void Awake () { }

        private void InitRef () { }
        public void OnMoveArrow ()
        {
            arrowBodyTransform.parent.gameObject.SetActive (true);
        }
        /**
         *   Animal '이동'시 화살표를 표시
         *   @param dir  각도
         *   @param dist 거리
         */
        public void VisualizeMoveArrow (Vector3 dir, float dist)
        {
            arrowBodyTransform.rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg);
            arrowBodyTransform.localScale = new Vector3 (dist * 2f,
                transform.localScale.y,
                transform.localScale.z);
            arrowEdgeTransform.rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg);
            arrowEdgeTransform.localPosition = new Vector3 (dist * dir.x, dist * dir.y, 0f);
        }
        public void OffMoveArrow ()
        {
            arrowBodyTransform.parent.gameObject.SetActive (false);
        }
        public void OnSelectMark()
        {
            selectMark.SetActive(true);
        }
        public void OffSelectMark()
        {
            selectMark.SetActive(false);
        }
    }
}
