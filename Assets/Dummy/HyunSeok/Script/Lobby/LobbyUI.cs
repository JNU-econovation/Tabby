using System.Collections;
using System.Collections.Generic;
using GameData;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    #region private field
    private enum EPanelState { LOGO, SELECT, DATA }

    [SerializeField]
    private EPanelState panelState;
    private Coroutine panelMoveCrtn;
    private EPanelState PanelState
    {
        get => panelState;
        set
        {
            panelState = value;
            if (panelMoveCrtn != null)
                StopCoroutine (panelMoveCrtn);
            panelMoveCrtn = StartCoroutine (OnPanel (panelState));
        }
    }

    [SerializeField]
    private RectTransform panelTransform;
    #endregion
    #region public field
    public List<LobbyPlayerData> playerDatas;
    #endregion
    #region reference field
    [SerializeField]
    private GameObject overlayPanel;
    [SerializeField]
    private CreatePlayerPanel createPlayerPanel;

    #endregion
    #region unity method
    void Start ()
    {
        Init ();
        PanelState = EPanelState.DATA;
    }
    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.Q))
        {
            PanelState = EPanelState.LOGO;
        }

        if (Input.GetKeyDown (KeyCode.W))
        {
            PanelState = EPanelState.SELECT;
        }
        if (Input.GetKeyDown (KeyCode.E))
        {
            PanelState = EPanelState.DATA;
        }
    }
    #endregion
    #region custom method
    /**
     *   LobbyUI Start 초기화
     */
    private void Init ()
    {
        for (int i = 0; i < 3; i++)
        {
            playerDatas[i].Init (DataManager.dataManager.playerDatas[i]);
        }
    }

    private IEnumerator OnPanel (EPanelState state)
    {
        Vector2 startPos = panelTransform.anchoredPosition;
        Vector2 endPos = new Vector2 ((int) state * -720, 0);
        float process = 0f;
        while (process < 1f)
        {
            panelTransform.anchoredPosition = Vector2.Lerp (startPos, endPos, process * 2f);
            process += Time.deltaTime;
            yield return null;
        }
    }

    public void OffOverlayPanel ()
    {
        // 하위 패널 비활성화
        createPlayerPanel.panel.SetActive (false);
        // 오버레이 패널 비활성화
        overlayPanel.SetActive (false);
    }
    public void OnClickData (int index)
    {
        // 만약 해당 데이터가 존재하지 않을 경우
        if (playerDatas[index].data.isNull)
        {
            // 새로운 플레이어 창 띄우기
            overlayPanel.SetActive (true);
            createPlayerPanel.index = index;
            createPlayerPanel.panel.SetActive (true);
            Debug.Log ("새로운 플레이어 생성");
        }
        else
        {
            // 게임 룸으로 이동하기
        }
    }

    public void OnClickCreateBtn ()
    {
        if (createPlayerPanel.nameField.text != null)
        {
            PlayerData newData = new PlayerData (createPlayerPanel.nameField.text);
            string dataPath = "/PlayerData/" + createPlayerPanel.index + ".json";
            GameData.DataManager.SaveData (newData, dataPath);
            OffOverlayPanel ();
            GameData.DataManager.dataManager.Init ();
            Init ();
        }
    }

    public void OnClickDeleteBtn (int index)
    {
        string dataPath = "/PlayerData/" + index + ".json";
        if (GameData.DataManager.DeleteData (dataPath))
        {
            GameData.DataManager.dataManager.Init ();
            Init ();
        }
    }
    #endregion
}

/**
 *   Lobby에서 PlayerData를 고르는 UI 버튼 등의 집합
 */
[System.Serializable]
public class LobbyPlayerData
{
    public Image icon;
    public Text name;

    public PlayerData data;

    public void Init (PlayerData data)
    {
        this.data = data;
        if (data.isNull)
        {
            name.text = "데이터가 존재하지 않습니다";
        }
        else
        {
            name.text = data.name;
        }

    }
}

[System.Serializable]
public class CreatePlayerPanel
{
    public GameObject panel;
    public int index;
    public InputField nameField;
}
