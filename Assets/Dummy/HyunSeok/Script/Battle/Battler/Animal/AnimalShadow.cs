using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class AnimalShadow : MonoBehaviour
    {
        public float firstY;
        private void Start()
        {
            firstY = transform.position.y;
        }
        private void LateUpdate()
        {
            transform.position = new Vector3(transform.position.x, firstY);
        }
    }
}

