using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        public PlayerData playerData;
 
        public int regionIndex;
        public int[] gogoAnimalIndexes;

        public Tuple<int, int>[] animalExp;
        public int farmObjects;
        public bool isWin;



        #endregion
        #region unity method
        void Awake ()
        {
            // 싱글톤
            if (_instance == null)
                _instance = this;
            else
                Destroy(this.gameObject);

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
                PlayerData initData = new PlayerData();
                initData.animalDatas.Add(new AnimalData(0, "얼룩이", 0));
                SaveData<PlayerData>(initData, "/PlayerData/" + 0 + ".json");

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
            /*SaveData<PlayerData>(new PlayerData(), "/PlayerData/" + 0 + ".json");
            SaveData<PlayerData>(new PlayerData(), "/PlayerData/" + 1 + ".json");
            SaveData<PlayerData>(new PlayerData(), "/PlayerData/" + 2 + ".json");*/
            DontDestroyOnLoad(this.gameObject);
            playerData = new PlayerData(LoadData("/PlayerData/" + 0 + ".json"));
            gogoAnimalIndexes = new int[3];
            for (int i = 0; i < 3; i++)
            {
                gogoAnimalIndexes[i] = -1;
            }
        }
        public void ParseAnimalDate(List<Animal> animals)
        {
            List<AnimalData> parseAnimalData = new List<AnimalData>();
            foreach (Animal animal in animals)
            {
                AnimalData temp = new AnimalData(animal.animalNumber, animal.animalName, animal.exp);
                parseAnimalData.Add(temp);

            }
            playerData.animalDatas = parseAnimalData;
            SaveData<PlayerData>(playerData, "/PlayerData/"+playerData.index+".json");
        }

        public void SaveMoney(int argMoney, int argHeart)
        {
            playerData.money = argMoney;
            playerData.heart = argHeart;
            SaveData<PlayerData>(playerData, "/PlayerData/" + playerData.index + ".json");
            Debug.Log("저장됨" + playerData.money);
        }

        public void ParseFarmObjectData(List<FarmObject> farmObjects)
        {
            List<FarmObjectData> parseFarmObjectData = new List<FarmObjectData>();
            foreach (FarmObject farmObject in farmObjects)
            {
                FarmObjectData temp = new FarmObjectData(farmObject.farmObjectNumber, farmObject.posX, farmObject.posY, farmObject.harvestTime, farmObject.isField);
                parseFarmObjectData.Add(temp);

            }
            playerData.farmObjectDatas = parseFarmObjectData;
            SaveData<PlayerData>(playerData, "/PlayerData/" + playerData.index + ".json");
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
            File.WriteAllText (dataPath + "/Data" + path, saveData.ToString (), Encoding.UTF8);
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
            string filePath = dataPath + "/Data" + path;
            if (File.Exists (filePath))
            {
                string jsonStr = File.ReadAllText (filePath);
                loadData = JsonMapper.ToObject (jsonStr);
                return (loadData);
            }
            else
            {
                Debug.Log ("File " + path + " does not exist");
                PlayerData initData = new PlayerData();
                initData.animalDatas.Add(new AnimalData(0, "얼룩이", 0));
                SaveData<PlayerData>(initData, "/PlayerData/" + 0 + ".json");
                string jsonStr = File.ReadAllText(filePath);
                loadData = JsonMapper.ToObject(jsonStr);
                return (loadData);
            }
        }

        public bool DeleteData (string path)
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
                PlayerData initData = new PlayerData();
                initData.animalDatas.Add(new AnimalData(0, "얼룩이", 0));
                SaveData<PlayerData>(initData, "/PlayerData/" + 0 + ".json");
                return (true);
            }
            else
            {
                return (false);
            }
        }

        public void DeletePlayerData()
        {
            Debug.Log(DeleteData("/PlayerData/" + 0 + ".json"));
        }
        #endregion
    }

    /**
     *   Player의 정보가 담긴 클래스
     */


    [System.Serializable]
    public class PlayerData
    {
        public int index;
        public string name;
        public int reputation;
        public int leaderIndex;
        public int money;
        public int heart;
        public List<AnimalData> animalDatas;
        public List<FarmObjectData> farmObjectDatas;

        public PlayerData()
        {
            index = 0;
            name = "";
            reputation = 0;
            leaderIndex = 0;
            money = 0;
            heart = 0;
            animalDatas = new List<AnimalData>();
            farmObjectDatas = new List<FarmObjectData>();
        }
        public PlayerData(JsonData data)
        {

            reputation = 0;
            leaderIndex = 0;
            money = int.Parse(data["money"].ToString()); 
            heart = int.Parse(data["heart"].ToString());
            animalDatas = new List<AnimalData>();
            farmObjectDatas = new List<FarmObjectData>();
            foreach (JsonData animalData in data["animalDatas"])
            {
                int index = int.Parse(animalData["index"].ToString());
                string animalName = (animalData["animalName"].ToString());
                int exp = int.Parse(animalData["exp"].ToString());
                animalDatas.Add(new AnimalData(index, animalName, exp));
            }
            foreach (JsonData farmObjectData in data["farmObjectDatas"])
            {
                int index = int.Parse(farmObjectData["index"].ToString());
                double posX = double.Parse(farmObjectData["posX"].ToString());
                double posY = double.Parse(farmObjectData["posY"].ToString());
                DateTime harvestTime = Convert.ToDateTime(farmObjectData["harvestTime"].ToString());
                bool isField = bool.Parse(farmObjectData["isField"].ToString());
                farmObjectDatas.Add(new FarmObjectData(index, posX, posY, harvestTime, isField));
            }


        }

        
    }
    [System.Serializable]
    public class AnimalData
    {
        public int index;
        public string animalName;
        public int exp;

        public AnimalData(int argIndex, string argName, int argExp)
        {
            index = argIndex;
            animalName = argName;
            exp = argExp;
        }
    }
    [System.Serializable]
    public class FarmObjectData
    {
        public int index;
        public double posX;
        public double posY;
        public DateTime harvestTime;
        public bool isField;

        public FarmObjectData(int argIndex, double argPosX, double argPosY, DateTime argHarvestTime, bool argIsField)
        {
            index = argIndex;
            posX = argPosX;
            posY = argPosY;
            harvestTime = argHarvestTime;
            isField = argIsField;
        }
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
