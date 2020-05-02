using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class DataManager : MonoBehaviour
{
    public PlayerData playerData;
    void Awake()
    {
        playerData = new PlayerData("현석", 100);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveData(1, "zz");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(LoadData(1, "zz"));
        }
    }
    /**
    *   json 데이터 저장
    *   @param index        몇번 째 세이브 파일인지
    *   @param path         현재 경로, 추후 수정 예정, 플랫폼 별 다르게 하기
    */
    public void SaveData(int index, string path)
    {
        JsonData saveData = JsonMapper.ToJson(playerData);
        string dataPath;
        Debug.Log(Application.platform);
        if (Application.platform == RuntimePlatform.WindowsEditor)
            dataPath = Application.dataPath;
        else
            dataPath = Application.persistentDataPath;
        File.WriteAllText(dataPath + "/Resources/TempData.json", saveData.ToString());
    }
    /**
    *   json 데이터 불러오기
    *   @param index        몇번 째 세이브 파일인지
    *   @param path         현재 경로, 추후 수정 예정, 플랫폼 별 다르게 하기
    */
    public IEnumerator LoadData(int index, string path)
    {
        string dataPath;
        if (Application.platform == RuntimePlatform.WindowsEditor)
            dataPath = Application.dataPath;
        else
            dataPath = Application.persistentDataPath;
        string filePath = dataPath + "/Resources/TempData.json";
        if(File.Exists(filePath))
        {
            string jsonStr = File.ReadAllText(filePath);
            JsonData loadData = JsonMapper.ToObject(jsonStr);
            Debug.Log(loadData["name"]);
        }
        yield return null;
    }
}

public class PlayerData
{
    public string name;
    public int gold;

    public PlayerData(string name, int gold)
    {
        this.name = name;
        this.gold = gold;
    }
}
