using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapButtonManager : MonoBehaviour
{
    public GameObject map;
    public GameObject mapButton;
    public GameObject mapCloseButton;
    public GameObject mapArea1;
    public GameObject mapArea2;
    public GameObject mapArea3;
    public GameObject ReadyExplore;

    public GameObject mapPrevealImage;
    public Sprite area1PrevealSprite;
    public Sprite area2PrevealSprite;
    public Sprite area3PrevealSprite;
    private Image prevearChanger;

    public void openMap()
    {
        map.gameObject.SetActive(true);
        mapButton.gameObject.SetActive(false);
        mapCloseButton.gameObject.SetActive(true);

    }

    public void closeMap()
    {
        if (ReadyExplore.activeSelf == false)
        {
            map.gameObject.SetActive(false);
            mapButton.gameObject.SetActive(true);
            mapCloseButton.gameObject.SetActive(false);
        }
        if (ReadyExplore.activeSelf == true)
        {
            ReadyExplore.gameObject.SetActive(false);
        }
    }

    public void Area1ReadyOpen()
    {
        ReadyExplore.gameObject.SetActive(true);
        prevearChanger = mapPrevealImage.GetComponent<Image>();
        prevearChanger.sprite = area1PrevealSprite;
    }
    public void Area2ReadyOpen()
    {
        ReadyExplore.gameObject.SetActive(true);
        prevearChanger = mapPrevealImage.GetComponent<Image>();
        prevearChanger.sprite = area2PrevealSprite;

    }
    public void Area3ReadyOpen()
    {
        ReadyExplore.gameObject.SetActive(true);
        prevearChanger = mapPrevealImage.GetComponent<Image>();
        prevearChanger.sprite = area3PrevealSprite;

    }
}
