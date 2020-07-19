using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Lobby
{
    public class LobbyUIManager : MonoBehaviour
    {
        public Text pressText;
        public Button pressButton;

        private void Start()
        {
            pressText.gameObject.SetActive(false);
            pressButton.enabled = false;
            StartCoroutine(OnPress());
        }

        IEnumerator OnPress()
        {
            yield return new WaitForSeconds(2.0f);
            pressText.gameObject.SetActive(true);
            pressButton.enabled = true;
        }

        public void OnClickPressKey()
        {
            SceneControl._instance.LoadTargetScene("AmaMain");
            pressButton.enabled = false;
        }
    }
}

