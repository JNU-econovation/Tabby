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
        heartText.text = heart.ToString();
        moneyText.text = money.ToString();
    }

    public static void HeartUP()
    {
        heart += 1;
        PlayerPrefs.SetInt("Heart", heart);
        //Json저장으로 수정필요
    }

    public static void HeartDown(int used)
    {
        heart -= used;
        PlayerPrefs.SetInt("Heart", heart);
    }

    public static void MoneyDown(int used)
    {
        money -= used;
        PlayerPrefs.SetInt("Money", money);
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
