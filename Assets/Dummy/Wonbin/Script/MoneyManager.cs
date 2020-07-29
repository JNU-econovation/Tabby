using GameData;
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
        money = DataManager._instance.playerData.money;
        heart = DataManager._instance.playerData.heart;
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
        DataManager._instance.playerData.heart=heart;
        //Json저장으로 수정필요
    }

    public static void HeartDown(int used)
    {
        heart -= used;
        DataManager._instance.playerData.heart=heart;
    }

    public static void MoneyDown(int used)
    {
        money -= used;
        DataManager._instance.playerData.money=money;
    }


    public static void MoneyUP(int sum){
        money += sum;
        DataManager._instance.playerData.money=money;
        //Json저장으로 수정필요

    }

    public void developerPower()
    {
    }
}
