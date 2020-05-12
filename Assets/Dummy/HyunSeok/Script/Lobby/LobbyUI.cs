using System.Collections;
using System.Collections.Generic;
using GameData;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    public List<LobbyPlayerData> playerDatas;

    void Start ()
    {
        Init ();
    }
    void Init ()
    {
        for (int i = 0; i < 3; i++)
        {
            playerDatas[i].Init(DataManager.dataManager.playerDatas[i]);
        }
    }
}

[System.Serializable]
public class LobbyPlayerData
{
    public Image icon;
    public Text name;

    public void Init (PlayerData data)
    {
        if (data != null)
        {
            name.text = data.name;
        }

    }
}
