using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    // 몇 번째 플레이어 데이터인가
    private int playerIdx;



    public int PlayerIdx { get => playerIdx; set => playerIdx = value; }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    
}

