using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

namespace GameData
{
    public class DataManager : MonoBehaviour
    {
        #region delegate event
        public delegate void DataEvent ();
        public event DataEvent eventAfterInit;
        #endregion
        #region public field
        // 싱글톤
        public static DataManager dataManager;
        public List<PlayerData> playerDatas;
        #endregion
        #region unity method
        void Awake ()
        {
            // 싱글톤
            if (dataManager == null)
                dataManager = this;

            // 없을 경우 data 폴더 만들어주기
            string dataPath = null;
            if (Application.platform == RuntimePlatform.WindowsEditor)
                dataPath = Application.dataPath;
            else
                dataPath = Application.persistentDataPath;
            DirectoryInfo dataDi = new DirectoryInfo (dataPath + "/Data");
            if (!dataDi.Exists)
            {
                dataDi.Create ();
            }
            DirectoryInfo playerDataDi = new DirectoryInfo (dataPath + "/Data/PlayerData");
            if (!playerDataDi.Exists)
            {
                playerDataDi.Create ();
            }

            Init ();
        }
        #endregion
        #region custom method
        /**
         *   DataManager Awake 초기화
         */
        public void Init ()
        {
            playerDatas = new List<PlayerData> (3);
            for (int i = 0; i < 3; i++)
            {
                playerDatas.Add (new PlayerData (LoadData ("/PlayerData/" + i.ToString () + ".json")));
            }
        }
        /**
         *   json 데이터 저장
         *   @param index        몇번 째 세이브 파일인지
         *   @param path         저장할 경로, ex) /PlayerData/FirstData/first.json
         */
        public static void SaveData<T> (T data, string path)
        {
            JsonData saveData = JsonMapper.ToJson (data);
            string dataPath = null;
            // 플랫폼에 따라 경로 변경
            if (Application.platform == RuntimePlatform.WindowsEditor)
                dataPath = Application.dataPath;
            else
                dataPath = Application.persistentDataPath;
            File.WriteAllText (dataPath + "/Data" + path, saveData.ToString ());
        }
        /**
         *   json 데이터 불러오기
         *   @param index        몇번 째 세이브 파일인지
         *   @param path         불러올 경로
         */
        public static JsonData LoadData (string path)
        {
            JsonData loadData = null;
            string dataPath = null;
            if (Application.platform == RuntimePlatform.WindowsEditor)
                dataPath = Application.dataPath;
            else
                dataPath = Application.persistentDataPath;
            string filePath = dataPath + "/Data" + path;
            Debug.Log (filePath);
            if (File.Exists (filePath))
            {
                string jsonStr = File.ReadAllText (filePath);
                loadData = JsonMapper.ToObject (jsonStr);
                return (loadData);
            }
            else
            {
                Debug.Log ("File " + path + " does not exist");
                return (null);
            }
        }

        public static bool DeleteData (string path)
        {
            string dataPath = null;
            if (Application.platform == RuntimePlatform.WindowsEditor)
                dataPath = Application.dataPath;
            else
                dataPath = Application.persistentDataPath;
            string filePath = dataPath + "/Data" + path;
            if (File.Exists (filePath))
            {
                File.Delete (filePath);
                return (true);
            }
            else
            {
                return (false);
            }
        }
        #endregion
    }

    /**
     *   Player의 정보가 담긴 클래스
     */
    public class PlayerData
    {
        public string name;
        public List<MonsterData> monsters;

        public DateTime a;
        public bool isNull;
        public PlayerData (string name)
        {
            a = new DateTime ();
            monsters = new List<MonsterData> ();
            monsters.Add (new MonsterData ("Tabby"));
            monsters.Add (new MonsterData ("Cat"));
            this.name = name;
        }
        public PlayerData (JsonData data)
        {
            if (data != null)
            {
                name = data["name"].ToString ();
            }
            else
            {
                isNull = true;
            }
        }
    }

    
    [System.Serializable]
    public class MonsterData
    {
        public string name;
        public int locX;
        public MonsterData (string name)
        {
            this.name = name;
            this.locX = 2;
        }
    }
}


