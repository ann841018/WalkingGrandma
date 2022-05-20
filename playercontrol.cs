using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playercontrol : MonoBehaviour 
{ 
	public Text[] Sco;
	public Text YourScore;

	int rankup;//第幾名
	bool RankOpen = false,Open = true,loseLife = true;
	public GameObject panel,RANK;

	// Use this for initialization
	void Start () 
	{
		panel.SetActive (false);
		RANK.SetActive (false);

		if (!PlayerPrefs.HasKey("checksave1"))//判定是否有存"checksave1"這種資料，沒有就進入if，並製作原始的排行榜
		{
			PlayerPrefs.SetInt("checksave1", 1);//寫入"checksave1"這種資料，避免每次都要製作原始的排行榜
			for (int i = 1; i <= 10; i++){PlayerPrefs.SetInt("rankscore" + i, 0);}
		}  
		for (int i = 1; i <= 10; i++) {	Sco [i - 1].text = PlayerPrefs.GetInt ("rankscore" + i).ToString ();}
	}

	void Update()
	{
		if (Gandma.gameOver == true) 
		{
			if (Open == true){RankOpen = true;}
			for (int i = 1; i <= 10; i++) 
			{
				if (Score.scoreInt > PlayerPrefs.GetInt ("rankscore" + i) && (Score.scoreInt != 0)) 
				{ 
					rankup = i;//當次玩得到第幾名
					break;//跳出迴圈
				}else rankup = 0;
			}
			if (loseLife == true) 
			{
				if (Score.life == 10) {Score.life = 9;PlayerPrefs.SetInt ("Life", 9);loseLife = false;}
				else if (Score.life == 9) {Score.life = 8;PlayerPrefs.SetInt ("Life", 8);loseLife = false;} 
				else if (Score.life == 8) {Score.life = 7;PlayerPrefs.SetInt ("Life", 7);loseLife = false;} 
				else if (Score.life == 7) {Score.life = 6;PlayerPrefs.SetInt ("Life", 6);loseLife = false;} 
				else if (Score.life == 6) {Score.life = 5;PlayerPrefs.SetInt ("Life", 5);loseLife = false;} 
				else if (Score.life == 5) {Score.life = 4;PlayerPrefs.SetInt ("Life", 4);loseLife = false;} 
				else if (Score.life == 4) {Score.life = 3;PlayerPrefs.SetInt ("Life", 3);loseLife = false;} 
				else if (Score.life == 3) {Score.life = 2;PlayerPrefs.SetInt ("Life", 2);loseLife = false;} 
				else if (Score.life == 2) {Score.life = 1;PlayerPrefs.SetInt ("Life", 1);loseLife = false;} 
				else if (Score.life == 1) {Score.life = 0;PlayerPrefs.SetInt ("Life", 0);loseLife = false;} 
				else {}
			}
			Time.timeScale = 0;
			panel.SetActive (true);
			RANK.SetActive (true);
			if (Input.anyKey) 
			{
				StartCoroutine (delay_rank_false ());
				Time.timeScale = 1;
				SceneManager.LoadScene (0);
				Gandma.gameOver = false;
			}
			//ConnectController.tapGestureAction = OnBack;
			if(menu.Keybo_link == true){
  				KeyboConnectInterface.SwitchTapGestureRecognizer (true, (state, type, x, y) => {KeyboConnectInterface.Log ("Tap In");
					if (state == (int)KeyboConnectInterface.TouchState.Begin && type != (int)KeyboConnectInterface.Taptype.Twice) {
						Time.timeScale = 1;
						SceneManager.LoadScene (0);
						Gandma.gameOver = false;
					}
				});
			}
		}
		if (RankOpen == true ) 
		{
			if (rankup > 0&&rankup<=10) {
				for (int i = 10; i >= rankup; i--) 
				{
					if (i >= rankup && Score.scoreInt != 0) 
					{
						PlayerPrefs.SetInt ("rankscore" + (i + 1), PlayerPrefs.GetInt ("rankscore" + i));
						//print (PlayerPrefs.GetInt ("rankscore" + i));
					}//將在當次得分以下的原來分數往下移動一名
					if (i == rankup && Score.scoreInt != 0) {PlayerPrefs.SetInt ("rankscore" + i, Score.scoreInt);}//寫入當次得分
				}
			}

			YourScore.text = Score.scoreInt.ToString ();
			RankOpen = false;
			Open = false;
			for (int i = 1; i <= 10; i++) {Sco [i - 1].text = PlayerPrefs.GetInt ("rankscore" + i).ToString ();	print (i+"   "+PlayerPrefs.GetInt ("rankscore" + i));}
		}
	}
	IEnumerator delay_rank_false() { yield return new WaitForSeconds(0.15f);panel.SetActive (false);}
}
