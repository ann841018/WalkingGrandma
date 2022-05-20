using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{
	public AudioSource music1,music2,music3,music4,music5;

	public Text UIscore,UImoney;

	public GameObject music01, music02,music03, music04,music05;
	public GameObject grandma,grandma_baseball,grandma_doll,grandma_rogue,grandma_zombie;

	float score;
	public static float time;
	public static int scoreInt,money,life=10;

	void Update()
	{
		if (life != 10) {time = time - Time.deltaTime;PlayerPrefs.SetInt ("Time", (int)time);PlayerPrefs.Save ();}
		if (life >= 10) {life = 10;time = 180;}
		if (time <= 0)  {life = life + 1;time = 180;}
	}

	void FixedUpdate ()
	{
		score = score + 0.04f;
		scoreInt = (int)score;
		UIscore.text = scoreInt.ToString ();
		UImoney.text = money.ToString ();

		if (menu.musicNumber == 0) 
		{
			music01.SetActive (true);music02.SetActive (false);music03.SetActive (false);music04.SetActive (false);music05.SetActive (false);
			if (score >= 100) {	music1.pitch = 1.111f;} else {music1.pitch = 0.9991f;}
		}else if (menu.musicNumber == 1) 
		{
			music02.SetActive (true);music01.SetActive (false);music03.SetActive (false);music04.SetActive (false);music05.SetActive (false);
			if (score >= 100){music2.pitch =1.111f;}else {music2.pitch = 0.9991f;}
		}else if (menu.musicNumber == 2) 
		{
			music03.SetActive (true);music01.SetActive (false);music02.SetActive (false);music04.SetActive (false);music05.SetActive (false);
			if (score >= 100){music3.pitch =1.111f;}else {music3.pitch = 0.9991f;}
		}else if (menu.musicNumber == 3) 
		{
			music04.SetActive (true);music01.SetActive (false);music02.SetActive (false);music03.SetActive (false);music05.SetActive (false);
			if (score >= 100){music4.pitch =1.111f;}else {music4.pitch = 0.9991f;}
		}else if (menu.musicNumber == 4) 
		{
			music05.SetActive (true);music01.SetActive (false);music02.SetActive (false);music03.SetActive (false);music04.SetActive (false);
			if (score >= 100){music5.pitch =1.111f;}else {music5.pitch = 0.9991f;}
		}

		if(menu.grandmaNumber == 0) {grandma.SetActive (true);grandma_baseball.SetActive (false);grandma_doll.SetActive (false);grandma_rogue.SetActive (false);grandma_zombie.SetActive (false);}
		else if(menu.grandmaNumber == 1){grandma.SetActive (false);grandma_baseball.SetActive (false);grandma_doll.SetActive (false);grandma_rogue.SetActive (true);grandma_zombie.SetActive (false);}
		else if(menu.grandmaNumber == 2){grandma.SetActive (false);grandma_baseball.SetActive (false);grandma_doll.SetActive (false);grandma_rogue.SetActive (false);grandma_zombie.SetActive (true);} 
		else if(menu.grandmaNumber == 3){grandma.SetActive (false);grandma_baseball.SetActive (true);grandma_doll.SetActive (false);grandma_rogue.SetActive (false);grandma_zombie.SetActive (false);} 
		else if(menu.grandmaNumber == 4){grandma.SetActive (false);grandma_baseball.SetActive (false);grandma_doll.SetActive (true);grandma_rogue.SetActive (false);grandma_zombie.SetActive (false);} 
	}
}
