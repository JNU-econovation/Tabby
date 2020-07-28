using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class AnimalShadow : MonoBehaviour
    {
        public float firstY;
        public float minus;
        private void Start()
        {
            firstY = transform.position.y;
            minus = transform.position.x - transform.parent.transform.position.x;
        }
        private void LateUpdate()
        {
            transform.position = new Vector3(transform.parent.transform.position.x + minus, firstY);
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}

