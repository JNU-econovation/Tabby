using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmObjectController : MonoBehaviour
{
    FarmObject farmObject;
    float producePeriod;
    float elapsedTime;
    
    public bool timePause;
    GameObject producing;
    GameObject harvestable;

    void Start()
    {
        producing = gameObject.transform.GetChild(0).gameObject;
        harvestable = gameObject.transform.GetChild(1).gameObject;

        farmObject = gameObject.GetComponent<FarmObject>();
        producePeriod = farmObject.producePeriod;
        elapsedTime = 0f;
        producing.gameObject.SetActive(true);
        harvestable.gameObject.SetActive(false);
        
    }


    void Update()
    {
        if (timePause != true)
        {
            if (elapsedTime < producePeriod)
                elapsedTime += Time.deltaTime;
            if (elapsedTime > producePeriod)
            {
                producing.gameObject.SetActive(false);
                timePause = true;
                harvestable.gameObject.SetActive(true);
                elapsedTime = 0f;
            }
        }
    

    }


}
