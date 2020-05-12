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
        public delegate void DataEvent();
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
            if (dataManager == null)
                dataManager = this;
            Init();
        }
        #endregion
        #region custom method
        private void Init ()
        {
            SaveData(new PlayerData("현석"), "/Data/PlayerData/0.json");
            SaveData(new PlayerData("원빈"), "/Data/PlayerData/1.json");
            SaveData(new PlayerData("고양이"), "/Data/PlayerData/2.json");
            playerDatas = new List<PlayerData> (3);
            for (int i = 0; i < 3; i++)
            {
                playerDatas.Add(new PlayerData (LoadData ("/Data/PlayerData/" + i.ToString () + ".json")));
            }
        }
        /**
         *   json 데이터 저장
         *   @param index        몇번 째 세이브 파일인지
         *   @param path         저장할 경로, ex) /PlayerData/FirstData/first.json
         */
        public void SaveData<T> (T data, string path)
        {
            JsonData saveData = JsonMapper.ToJson (data);
            string dataPath = null;
            // 플랫폼에 따라 경로 변경
            if (Application.platform == RuntimePlatform.WindowsEditor)
                dataPath = Application.dataPath;
            else
                dataPath = Application.persistentDataPath;
            File.WriteAllText (dataPath + "/Resources" + path, saveData.ToString ());
        }
        /**
         *   json 데이터 불러오기
         *   @param index        몇번 째 세이브 파일인지
         *   @param path         불러올 경로
         */
        public JsonData LoadData (string path)
        {
            JsonData loadData = null;
            string dataPath = null;
            if (Application.platform == RuntimePlatform.WindowsEditor)
                dataPath = Application.dataPath;
            else
                dataPath = Application.persistentDataPath;
            string filePath = dataPath + "/Resources" + path;
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
        #endregion
    }

    public class PlayerData
    {
        public string name;
        public PlayerData(string name)
        {
            this.name = name;
        }
        public PlayerData (JsonData data)
        {
            if (data != null)
            {
                name = data["name"].ToString ();
            }
        }
    }
}
