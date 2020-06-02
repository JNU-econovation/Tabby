using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    private GameObject farmObjectShop;

    public void pathStart()
    {
        farmObjectShop = GameObject.Find("farmObShop");
        if (farmObjectShop != null)
            BroadcastMessage("pathFindingStart");
    }
}

//TapZone 아니면 money+1 되도록 inputManager