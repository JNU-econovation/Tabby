using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
	
}

public class GameTime
{
	private int year;
	private int month;
	private int day;
	private int hour;
	private int minute;
	private int second;

	GameTime(int year, int month, int day, int hour, int minute, int second)
	{
		this.year		= year;
		this.month		= month;
		this.day		= day;
		this.hour		= hour;
		this.minute		= minute;
		this.second		= second;
	}

	public int SetTimer(int year, int month, int day, int hour, int minute, int second)
	{
		GameTime currentTime = new GameTime(this.year, this.month, this.day, this.hour, this.minute, this.second);
		return (0);
	}
}
