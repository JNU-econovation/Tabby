using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class heart : MonoBehaviour
{
    Rigidbody2D heartgoing;
    private int speed;

    void Start()
    {
        
    }

    void Update()
    {
        //heartgoing.position = Vector2.MoveTowards(heartgoing.position, new Vector2(parent.position.x, parent.position.y), speed*Time.deltaTime);
    }
}
