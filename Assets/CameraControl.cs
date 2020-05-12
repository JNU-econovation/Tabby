using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void shopOpened()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 5.5f, transform.position.z);
    }

    public void shopClosed()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 5.5f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
