using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScore : MonoBehaviour 
{
	public Text UImoney,UIlife,UItime;
	public GameObject music01, music02,music03, music04,music05;
	public DateTime OldTime, NewTime;
	public TimeSpan TimeSpan;
	public int During;
	public static int TotalMoney;

	void Start()
	{
		OldTime = new DateTime(PlayerPrefs.GetInt ("Year"), PlayerPrefs.GetInt ("Month"), PlayerPrefs.GetInt ("Day"), PlayerPrefs.GetInt ("Hour"), PlayerPrefs.GetInt ("Minute"), PlayerPrefs.GetInt ("Second"));
		NewTime = DateTime.Now;TimeSpan = NewTime.Subtract (OldTime);During = TimeSpan.Minutes * 60 + TimeSpan.Seconds;
		Score.money = PlayerPrefs.GetInt ("Money");
		if (During >= 1800) {Score.life = Score.life+10;Score.time = 180;}
		else if(During >= 1620) {Score.life = Score.life+9;Score.time = PlayerPrefs.GetInt ("Time") - (During - 1620);}
		else if(During >= 1440) {Score.life = Score.life+8;Score.time = PlayerPrefs.GetInt ("Time") - (During - 1440);}
		else if(During >= 1260) {Score.life = Score.life+7;Score.time = PlayerPrefs.GetInt ("Time") - (During - 1260);}
		else if(During >= 1080) {Score.life = Score.life+6;Score.time = PlayerPrefs.GetInt ("Time") - (During - 1080);}
		else if(During >= 900) {Score.life = Score.life+5;Score.time = PlayerPrefs.GetInt ("Time") - (During - 900);}
		else if(During >= 720) {Score.life = Score.life+4;Score.time = PlayerPrefs.GetInt ("Time") - (During - 720);}
		else if(During >= 540) {Score.life = Score.life+3;Score.time = PlayerPrefs.GetInt ("Time") - (During - 540);}
		else if(During >= 360) {Score.life = Score.life+2;Score.time = PlayerPrefs.GetInt ("Time") - (During - 360);}
		else if(During >= 180) {Score.life = Score.life+1;Score.time = PlayerPrefs.GetInt ("Time") - (During - 180);}
		else {Score.life = PlayerPrefs.GetInt ("Life");Score.time = PlayerPrefs.GetInt ("Time") - During;}
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		TotalMoney = Score.money;UImoney.text = TotalMoney.ToString ();

		if (menu.musicNumber == 0) {music01.SetActive (true);music02.SetActive (false);music03.SetActive (false);music04.SetActive (false);music05.SetActive (false);}
		else if (menu.musicNumber == 1) {music02.SetActive (true);music01.SetActive (false);music03.SetActive (false);music04.SetActive (false);music05.SetActive (false);} 
		else if (menu.musicNumber == 2)	{music03.SetActive (true);music01.SetActive (false);music02.SetActive (false);music04.SetActive (false);music05.SetActive (false);}
		else if (menu.musicNumber == 3) {music04.SetActive (true);music01.SetActive (false);music02.SetActive (false);music03.SetActive (false);music05.SetActive (false);}
		else if (menu.musicNumber == 4) {music05.SetActive (true);music01.SetActive (false);music02.SetActive (false);music03.SetActive (false);music04.SetActive (false);}

		if (Score.life != 10){Score.time = Score.time - Time.deltaTime;}
		if (Score.life >= 10) {Score.life = 10;Score.time = 180;}
		if (Score.time <= 0){Score.life = Score.life + 1;Score.time = 180;}

		TimeSpan timeSpan = TimeSpan.FromSeconds (Score.time);
		UItime.text = string.Format ("{0:D1}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
		UIlife.text = Score.life.ToString (); 

		PlayerPrefs.SetInt ("Year", DateTime.Now.Year);
		PlayerPrefs.SetInt ("Month", DateTime.Now.Month);
		PlayerPrefs.SetInt ("Day", DateTime.Now.Day);
		PlayerPrefs.SetInt ("Hour", DateTime.Now.Hour);
		PlayerPrefs.SetInt ("Minute", DateTime.Now.Minute);
		PlayerPrefs.SetInt ("Second", DateTime.Now.Second);

		PlayerPrefs.SetInt ("Score", Score.scoreInt);
		PlayerPrefs.SetInt ("Money", Score.money);
		PlayerPrefs.SetInt ("Life", Score.life);
		PlayerPrefs.SetInt ("Time", (int)Score.time);
	}
}
