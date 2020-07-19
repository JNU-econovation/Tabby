using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
    public GameObject loadingPanel;
    public void OnDisable()
    {
        loadingPanel.SetActive(false);
    }
}
