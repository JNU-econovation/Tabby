using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class CameraRatio : MonoBehaviour
    {
        private Camera cam;

        private float initSize;
        private float sizeX;
        private float sizeY;
        private float currentRatio;
        private float standardRatio;

        private void Awake()
        {
            cam = GetComponent<Camera>();
            initSize = cam.orthographicSize;
            sizeX = Screen.width;
            sizeY = Screen.height;
            standardRatio = 16f / 9f;
            currentRatio = (float)sizeX / sizeY;
            SetSize();
        }

        void SetSize()
        {
            cam.orthographicSize = initSize * (standardRatio) / currentRatio;
        }
    }
}

