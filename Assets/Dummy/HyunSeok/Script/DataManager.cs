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
        public static DataManager _instance;
        [SerializeField]
        public List<PlayerData> playerDatas = new List<PlayerData> ();
        public List<PlayerData> PlayerDatas { get => playerDatas; set => playerDatas = value; }
        #endregion
        #region unity method
        void Awake ()
        {
            // 싱글톤
            if (_instance == null)
                _instance = this;

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
        private void Init ()
        {
            /*playerDatas = new List<PlayerData> (3);
            for (int i = 0; i < 3; i++)
            {
                playerDatas.Add (new PlayerData (LoadData ("/PlayerData/" + i.ToString () + ".json")));
            }*/
            PlayerDatas.Add (new PlayerData (LoadData ("/PlayerData/0.json")));
        }

        public void SaveAnimals (List<Animal> animals)
        {

            PlayerDatas[GameManager._instance.PlayerIdx].animalDatas.Clear ();
            foreach (Animal animal in animals)
            {
                Debug.Log("dddd : " + animal.animalNumber);
                PlayerDatas[GameManager._instance.PlayerIdx].animalDatas.Add (new AnimalData (animal.animalNumber, animal.level, animal.exp));
            }
            SaveData<PlayerData> (PlayerDatas[GameManager._instance.PlayerIdx], "/PlayerData/0.json");
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
    [System.Serializable]
    public class AnimalData
    {
        public int idx;
        public int level;
        public int exp;

        public AnimalData (int idx, int level, int exp)
        {
            this.idx = idx;
            this.level = level;
            this.exp = exp;
        }
    }
    [System.Serializable]
    public class PlayerData
    {
        public string name;
        public int reputation;
        public int leaderIdx;
        public int money;
        public int heart;
        public DateTime playTime;
        public List<ItemLocationData> itemLocationDatas;
        public List<int> inventoryDatas;
        public List<AnimalData> animalDatas;

        public PlayerData ()
        {
            name = "test";
            reputation = 0;
            leaderIdx = 0;
            money = 0;
            heart = 0;
            playTime = new DateTime ();
            itemLocationDatas = new List<ItemLocationData> ();
            inventoryDatas = new List<int> ();
            animalDatas = new List<AnimalData> ();
        }
        public PlayerData (JsonData data)
        {
            name = "test";
            reputation = 0;
            leaderIdx = 0;
            money = 0;
            heart = 0;
            playTime = new DateTime ();
            itemLocationDatas = new List<ItemLocationData> ();
            inventoryDatas = new List<int> ();
            animalDatas = new List<AnimalData> ();
            //Debug.Log(data["animalDatas"][0].ToString());
            //AnimalData[] saveData = JsonHelper.FromJson<AnimalData> (data["animalDatas"][0].ToString());
            //Debug.Log(data["animalDatas"]);
            foreach (JsonData animalData in data["animalDatas"])
            {
                int idx = int.Parse (animalData["idx"].ToString ());
                int level = int.Parse (animalData["level"].ToString ());
                int exp = int.Parse(animalData["exp"].ToString());
                animalDatas.Add (new AnimalData (idx, level, exp));
            }
        }

        //public List<AnimalData> monsters;

    }

    [System.Serializable]
    public class ItemLocationData
    {
        public int index;
        public int x;
        public int y;
        public bool isOut;
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T> (string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (json);
            return wrapper.Items;
        }

        public static string ToJson<T> (T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T> ();
            wrapper.Items = array;
            return JsonUtility.ToJson (wrapper);
        }

        public static string ToJson<T> (T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T> ();
            wrapper.Items = array;
            return JsonUtility.ToJson (wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }

    }
}
