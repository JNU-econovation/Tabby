using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

namespace Localize 
{
    public class LocalizeManager : MonoBehaviour 
    {
        #region static field
        public static string localeCode;
        public JsonData localeDataLobbyEN;

        #endregion
        #region static function
        public static void UpdateLocale (string code) 
        {

        }
        #endregion

        void Awake()
        {
            string filePath = File.ReadAllText(Application.dataPath + "/Resources/Localize/UI/localize_UI_Lobby_EN.json");
            Debug.Log(filePath);
            localeDataLobbyEN = JsonMapper.ToObject(filePath);
            Debug.Log(localeDataLobbyEN[0]["pressAnyKey"].ToString());
        }
    }

    [System.Serializable]
    public class LobbyData
    {
        public string pressAnyKey;
    }
}