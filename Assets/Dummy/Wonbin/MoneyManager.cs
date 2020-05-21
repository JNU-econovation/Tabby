using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyManager : MonoBehaviour
{
    static public int money;
    static public int heart;
    public Text heartText;
    public Text moneyText;
    void Start()
    {
        money = PlayerPrefs.GetInt("Money");
        heart = PlayerPrefs.GetInt("Heart");
    }
    void Update()
    {
        moneyText.text = "재화 : " + money;
        heartText.text = "하트 : " + heart;
    }

    public void OnMouseDown(){
        money += 1;
        PlayerPrefs.SetInt("Money", money);
        

    }

    public void developerPower()
    {
        money += 499;
        heart += 10;
    }
}
