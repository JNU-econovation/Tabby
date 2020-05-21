using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Drag : MonoBehaviour
    {

    float distance = 10;
    Rigidbody2D PIrigidbody;

    GameObject heartImage;

    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,
        Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    public void PItransformMid()
    {
        transform.position = new Vector3(0f, -1.5f, 0f);
    }

    public void PItransformBack()
    {
        transform.position = new Vector3(-30f, 0f, 0f);
    }

    public void heartDrag(Vector2 animal)
    {
        Instantiate(heartImage, animal, Quaternion.identity);
    }
}

