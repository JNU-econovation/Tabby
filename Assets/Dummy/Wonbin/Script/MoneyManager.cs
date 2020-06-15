using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyManager : MonoBehaviour
{
    public static int money;
    public static int heart;
    public Text moneyText;
    public Text heartText;
    void Start()
    {
        money = PlayerPrefs.GetInt("Money");
        heart = PlayerPrefs.GetInt("Heart");
        //Json불러오기로 수정
    }
    void Update()
    {
        heartText.text = "하트 : " + heart;
        moneyText.text = "재화 : " + money;
    }

    public static void HeartUP()
    {
        heart += 1;
        PlayerPrefs.SetInt("Money", heart);
        //Json저장으로 수정필요
    }


    public static void MoneyUP(int sum){
        money += sum;
        PlayerPrefs.SetInt("Money", money);
        //Json저장으로 수정필요

    }

    public void developerPower()
    {
        money += 499;
        heart += 10;
    }
}
