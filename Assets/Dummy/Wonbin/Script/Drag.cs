using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Drag : MonoBehaviour
    {

    static float distance = 10;
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
        transform.position = new Vector3(0f, -1.5f, -5f);
    }

    public void IItransformMid()
    {
        transform.position = new Vector3(0f, -1.5f, -5f);
    }

    public void PItransformBack()
    {
        transform.position = new Vector3(-30f, 0f, 0f);
    }
    public void IItransformBack()
    {
        transform.position = new Vector3(-500f, 0f, 0f);
    }

    public void heartDrag(Vector2 animal)
    {
        Instantiate(heartImage, animal, Quaternion.identity);
    }

    public void ChangePos(float posX, float posY)
    {
        gameObject.transform.position = new Vector2(posX, posY);

    }
    public static void AnimalDrag(Rigidbody2D rigidbody2D)
    {
        //animator.SetBool("tapAnimal", true); //뜬 애니메이션
        //드래그하면 들림. 커서를 따라 이동
        rigidbody2D.position = new Vector2(rigidbody2D.position.x, rigidbody2D.position.y + 3);
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,
        Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        rigidbody2D.position = objPosition;
        //animator.SetBool("tapAnimal", true);
    }
}

