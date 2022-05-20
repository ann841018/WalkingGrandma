using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuRank : MonoBehaviour 
{ 
	public Text[] Sco;
	public Text YourScore;

	// Use this for initialization

	void Update()
	{
		for (int i = 1; i <= 10; i++) {Sco [i - 1].text = PlayerPrefs.GetInt ("rankscore" + i).ToString ();}
		YourScore.text = Score.scoreInt.ToString ();
	}
}
