using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace GameTime
{
	public class TimeControl : MonoBehaviour
	{
		public GameTimer currentTimer;
	}

	public class GameTimer
	{
		public DateTime timer;
		public float speed;
	}
}

