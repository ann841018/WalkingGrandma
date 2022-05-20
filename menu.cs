using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class menu : MonoBehaviour 
{
	public RectTransform select_btn,select_skin,select_bg,select_music; // to hold the select_panel
	public GameObject menu_panel,skin_panel,bg_panel,music_panel; // to hold the meun_panel
	public Button[] btn;  //to hold the button list
	public Button[] skin;  //to hold the skin list
	public Button[] bg;  //to hold the background list
	public Button[] music;  //to hold the music_list
	public Image[] skin_img;
	public Image[] bg_img;
	public Image[] music_img;
	public Sprite[] skin_img_unlock;
	public Sprite[] bg_img_unlock;
	public Sprite[] music_img_unlock;

	public Text connect_state;
	public GameObject Text_Text;
	public GameObject RankUI,Panel,Rank;
	public GameObject BuyingUI,NoMoneyUI,NoLifeUI,QuestionUI;
	public Button Yes,No,YesBtn, NoBtn;//Buy

	private bool rank_isOpen = false;
	private bool noLife_isOpen = false;
	private bool buying_isOpen = false;
	private bool init_success = false;  //to hold the state of init
 	private bool bt_ready = false;  // to hold the state of the BlueTooth is ready
	private bool isPan = false;  //to hold the pan mode is working or not
	private bool is_click = false;  //to hold if the website had open or not
	private bool is_pause = false;  //to hold if the app was pause or not
	public static bool Keybo_link = false;

	public RectTransform center;  //center to compare the dis for each btn
	public AudioSource clickSound;  //for sound background
	public Animator meun_ani,skin_ani,bg_ani,music_ani;

	bool dragging = false;
	float panel_width;  //to hold the panel width
	int temp_min_num,panel_num = 1;  //to hold the which panel is active now, 1 for meun, 2 for skin, 3 for bg, 4 for music

	float[] distance_menu,disResposition_menu;//for main meun
	float[] distance_skin,disResposition_skin;//for skin select
	float[] distance_bg,disResposition_bg;//for background select
	float[] distance_music,disResposition_music;//for music select

	int btn_menu_distance,min_btn_menu_num,temp_menu=0,btn_menu_lenght;
	int btn_skin_distance,min_btn_skin_num,temp_skin=0,btn_skin_lenght;
	int btn_bg_distance,min_btn_bg_num,temp_bg=0,btn_bg_lenght;
	int btn_music_distance,min_btn_music_num,temp_music=0,btn_music_lenght;

	public static int grandmaNumber,backGroundNumber,musicNumber;//to send the value of skin

	public Text[] Skin,Background,Music;//商品的價格設定
	public GameObject[] SkinUI,BackgroundUI,MusicUI;//價錢跟鎖頭的UI
	public GameObject[] ShowSkin,ShowBackground;//顯示Skin

	public int[]SkinCost,BackgroundCost,MusicCost;//商品價錢的值
	public int LifeCost = 1000;
	public bool[] CanBuySkin,CanBuyBackground,CanBuyMusic;//是否可以購買商品
	public bool CanBuyLife;

	public static bool HaveSkin0 = true,HaveSkin1=false,HaveSkin2=false,HaveSkin3=false,HaveSkin4=false; 
	public static bool HaveBackground0 = true,HaveBackground1=false,HaveBackground2=false,HaveBackground3=false; 
	public static bool HaveMusic0 = true,HaveMusic1=false,HaveMusic2=false,HaveMusic3=false,HaveMusic4=false; 

	void Start()
	{
		//keybo
		//change the keybo mode to coordinate
		KeyboConnectInterface.Log ("Onstart");
		if (KeyboConnectInterface.Initialized) {StartCoroutine (changeMode ());	KeyboConnectInterface.Log ("Onstart Inited");
		} else {KeyboConnectInterface.Log ("Onstart Init");KeyboConnectInterface.Initialize (() => {
				KeyboConnectInterface.Log ("Init Success");
				connect_state.GetComponent<Text> ().text = "Init Success";
				init_success = true;
				StartCoroutine (connect ());
			}, (error) => {	KeyboConnectInterface.Log (error);});
		}

		//action set
		ConnectController.connectReturn = connectStateChange;
		ConnectController.connectError = connectError;
		ConnectController.panGestureAction = OnPan;
		ConnectController.tapGestureAction = OnTap;
		ConnectController.swipeGestureAction = OnSwipe;

		//set the animation
		meun_ani = GameObject.Find ("select_btn").GetComponent<Animator> ();
		skin_ani = GameObject.Find ("select_skin").GetComponent<Animator> ();
		bg_ani = GameObject.Find ("select_background").GetComponent<Animator> ();
		music_ani = GameObject.Find ("select_music").GetComponent<Animator> ();

		//get distance between btn
		btn_menu_distance = (int)Mathf.Abs (btn [1].GetComponent<RectTransform> ().anchoredPosition.x - btn [0].GetComponent<RectTransform> ().anchoredPosition.x);
		btn_skin_distance = (int)Mathf.Abs (skin [1].GetComponent<RectTransform> ().anchoredPosition.x - skin [0].GetComponent<RectTransform> ().anchoredPosition.x);
		btn_bg_distance = (int)Mathf.Abs (bg [1].GetComponent<RectTransform> ().anchoredPosition.x - bg [0].GetComponent<RectTransform> ().anchoredPosition.x);
		btn_music_distance = (int)Mathf.Abs (music [1].GetComponent<RectTransform> ().anchoredPosition.x - music [0].GetComponent<RectTransform> ().anchoredPosition.x);

		//for main meun
		menu_panel.SetActive (true);
		btn_menu_lenght = btn.Length;
		distance_menu = new float[btn_menu_lenght];
		disResposition_menu = new float[btn_menu_lenght];

		//for skin select
		skin_panel.SetActive (false);
		btn_skin_lenght = skin.Length;
		distance_skin = new float[btn_skin_lenght];
		disResposition_skin = new float[btn_skin_lenght];

		//for background select
		bg_panel.SetActive (false);
		btn_bg_lenght = bg.Length;
		distance_bg = new float[btn_bg_lenght];
		disResposition_bg = new float[btn_bg_lenght];

		//for music select
		music_panel.SetActive (false);
		btn_music_lenght = music.Length;
		distance_music = new float[btn_music_lenght];
		disResposition_music = new float[btn_music_lenght];

		//HaveSkin
		if (PlayerPrefs.GetInt ("HaveSkin1") == 1) {HaveSkin1 = true;SkinUI [1].SetActive (false);skin_img[1].sprite = skin_img_unlock[1];}
		if (PlayerPrefs.GetInt ("HaveSkin2") == 1) {HaveSkin2 = true;SkinUI [2].SetActive (false);skin_img[2].sprite = skin_img_unlock[2];}
		if (PlayerPrefs.GetInt ("HaveSkin3") == 1) {HaveSkin3 = true;SkinUI [3].SetActive (false);skin_img[3].sprite = skin_img_unlock[3];}
		if (PlayerPrefs.GetInt ("HaveSkin4") == 1) {HaveSkin4 = true;SkinUI [4].SetActive (false);skin_img[4].sprite = skin_img_unlock[4];}
		if (PlayerPrefs.GetInt ("HaveBackground1") == 1) {HaveBackground1 = true;BackgroundUI [1].SetActive (false);bg_img[1].sprite = bg_img_unlock[1];}
		if (PlayerPrefs.GetInt ("HaveBackground2") == 1) {HaveBackground2 = true;BackgroundUI [2].SetActive (false);bg_img[2].sprite = bg_img_unlock[2];}
		if (PlayerPrefs.GetInt ("HaveBackground3") == 1) {HaveBackground3 = true;BackgroundUI [3].SetActive (false);bg_img[3].sprite = bg_img_unlock[3];}
		if (PlayerPrefs.GetInt ("HaveMusic1") == 1) {HaveMusic1 = true;MusicUI [1].SetActive (false);music_img[1].sprite = music_img_unlock[1];}
		if (PlayerPrefs.GetInt ("HaveMusic2") == 1)	{HaveMusic2 = true;MusicUI [2].SetActive (false);music_img[2].sprite = music_img_unlock[2];}
		if (PlayerPrefs.GetInt ("HaveMusic3") == 1) {HaveMusic3 = true;MusicUI [3].SetActive (false);music_img[3].sprite = music_img_unlock[3];}
		if (PlayerPrefs.GetInt ("HaveMusic4") == 1) {HaveMusic4 = true;MusicUI [4].SetActive (false);music_img[4].sprite = music_img_unlock[4];}
	
		//set Cost
		BuyingUI.SetActive(false);NoMoneyUI.SetActive (false);NoLifeUI.SetActive (false);QuestionUI.SetActive(false);

		for (int i = 1; i <= 4; i++) 
		{
			Skin [i].text = SkinCost [i].ToString ();
			Music [i].text = MusicCost [i].ToString ();
		}
		for (int i = 1; i < 4; i++) 
		{
			Background [i].text = BackgroundCost [i].ToString ();
		}

//		//set the position of btns
//		skin_panel.GetComponent<RectTransform> ().anchoredPosition = new Vector2(300 * PlayerPrefs.GetInt("SelectSkin"),0);
//		bg [PlayerPrefs.GetInt("SelectBackground")].GetComponent<RectTransform> ().anchoredPosition = new Vector2(0,0);
//		music [PlayerPrefs.GetInt("SelectMusic")].GetComponent<RectTransform> ().anchoredPosition = new Vector2(0,0);
	}

	void Update()
	{
		//if the app was been pause, have to change back the keybo mode
		if (is_pause == true) {StartCoroutine (changeMode ());is_pause = false;}

		//get the btn height & width, only the bg have the different size
		panel_width = Screen.width / 1280f;
		switch (panel_num) {
		case 1:	meun_control ();break;
		case 2:	skin_control ();break;
		case 3:	bg_control ();break;
		case 4:	music_control ();break;}

		if (Score.money >= LifeCost) {CanBuyLife = true;} else CanBuyLife = false;
		for (int i = 1; i <= 4; i++) 
		{
			if (Score.money >= SkinCost [i]) {CanBuySkin [i] = true;} else CanBuySkin [i] = false;
			if (Score.money >= MusicCost[i]) {CanBuyMusic[i] = true;} else CanBuyMusic [i] = false;
		}
		for (int i = 1; i < 4; i++) 
		{
			if (Score.money >= BackgroundCost [i]){CanBuyBackground [i] = true;} else CanBuyBackground [i] = false;
		}
	}

	void meun_control()
	{
		for (int i = 0; i < btn.Length; i++) 
		{
			disResposition_menu [i] = center.GetComponent<RectTransform> ().position.x - btn [i].GetComponent<RectTransform> ().position.x;
			distance_menu [i] = Mathf.Abs (disResposition_menu [i]);
			if (disResposition_menu [i] > (1500 * panel_width)) 
			{
				float curX_meun = btn [i].GetComponent<RectTransform> ().anchoredPosition.x;
				float curY_meun = btn [i].GetComponent<RectTransform> ().anchoredPosition.y;                                                                                                                                                                                                                                                    
				Vector2 newAnchoredPos_meun = new Vector2 (curX_meun + (btn_menu_lenght * btn_menu_distance), curY_meun);
				btn [i].GetComponent<RectTransform> ().anchoredPosition = newAnchoredPos_meun;
			}
			if (disResposition_menu [i] < -(1500 * panel_width)) 
			{
				float curX_meun = btn [i].GetComponent<RectTransform> ().anchoredPosition.x;
				float curY_meun = btn [i].GetComponent<RectTransform> ().anchoredPosition.y;
				Vector2 newAnchoredPos_meun = new Vector2 (curX_meun - (btn_menu_lenght * btn_menu_distance), curY_meun);
				btn [i].GetComponent<RectTransform> ().anchoredPosition = newAnchoredPos_meun;
			}
		}

		float min_menu_distance = Mathf.Min (distance_menu);  //get the min dis
		for (int i = 0; i < btn.Length; i++) {if (min_menu_distance == distance_menu [i]) {min_btn_menu_num = i;}}
		if (rank_isOpen == true) {if (Input.anyKey) {clickSound.Play ();StartCoroutine (delay_rank_false());}}
		if (!dragging) {Lerp_to_meun_btn (-btn [min_btn_menu_num].GetComponent<RectTransform> ().anchoredPosition.x);}
		if (temp_menu != min_btn_menu_num) 
		{
			btn [temp_menu].interactable = false;
			btn [min_btn_menu_num].interactable = true;
			temp_menu = min_btn_menu_num;
		}
		if (min_btn_menu_num == 5) {Text_Text.SetActive (true);} 
		else {Text_Text.SetActive (false);}
	}

	void skin_control()
	{
		for (int i = 0; i < skin.Length; i++) 
		{
			disResposition_skin [i] = center.GetComponent<RectTransform> ().position.x - skin [i].GetComponent<RectTransform> ().position.x;
			distance_skin [i] = Mathf.Abs (disResposition_skin [i]);
			if (disResposition_skin [i] > (780*panel_width)) 
			{
				float curX_skin = skin [i].GetComponent<RectTransform> ().anchoredPosition.x;
				float curY_skin = skin [i].GetComponent<RectTransform> ().anchoredPosition.y;
				Vector2 newAnchoredPos_skin = new Vector2 (curX_skin + (btn_skin_lenght * btn_skin_distance), curY_skin);
				skin [i].GetComponent<RectTransform> ().anchoredPosition = newAnchoredPos_skin;
			}
			if (disResposition_skin [i] < -(780*panel_width)) 
			{
				float curX_skin = skin [i].GetComponent<RectTransform> ().anchoredPosition.x;
				float curY_skin = skin [i].GetComponent<RectTransform> ().anchoredPosition.y;
				Vector2 newAnchoredPos_skin = new Vector2 (curX_skin - (btn_skin_lenght * btn_skin_distance), curY_skin);
				skin [i].GetComponent<RectTransform> ().anchoredPosition = newAnchoredPos_skin;
			}
		}

		float min_skin_distance = Mathf.Min (distance_skin);  //get the min dis
		for (int i = 0; i < skin.Length; i++) {if (min_skin_distance == distance_skin [i]) {min_btn_skin_num = i;}}
		if (!dragging) {Lerp_to_skin_btn (-skin [min_btn_skin_num].GetComponent<RectTransform> ().anchoredPosition.x);}
		if (temp_skin != min_btn_skin_num) {skin [temp_skin].interactable = false;skin [min_btn_skin_num].interactable = true;temp_skin = min_btn_skin_num;}
	}

	void bg_control()
	{
		for (int i = 0; i < bg.Length; i++) 
		{
			disResposition_bg [i] = center.GetComponent<RectTransform> ().position.x - bg [i].GetComponent<RectTransform> ().position.x;
			distance_bg [i] = Mathf.Abs (disResposition_bg [i]);
			if (disResposition_bg [i] > (1550*panel_width)) 
			{
				float curX_bg = bg [i].GetComponent<RectTransform> ().anchoredPosition.x;
				float curY_bg = bg [i].GetComponent<RectTransform> ().anchoredPosition.y;
				Vector2 newAnchoredPos_bg = new Vector2 (curX_bg + (btn_bg_lenght * btn_bg_distance), curY_bg);
				bg [i].GetComponent<RectTransform> ().anchoredPosition = newAnchoredPos_bg;
			}
			if (disResposition_bg [i] < -(1550*panel_width)) 
			{
				float curX_bg = bg [i].GetComponent<RectTransform> ().anchoredPosition.x;
				float curY_bg = bg [i].GetComponent<RectTransform> ().anchoredPosition.y;
				Vector2 newAnchoredPos_bg = new Vector2 (curX_bg - (btn_bg_lenght * btn_bg_distance), curY_bg);
				bg [i].GetComponent<RectTransform> ().anchoredPosition = newAnchoredPos_bg;
			}
		}

		float min_bg_distance = Mathf.Min (distance_bg);  //get the min dis
		for (int i = 0; i < bg.Length; i++) {if (min_bg_distance == distance_bg [i]) {min_btn_bg_num = i;}}
		if (!dragging) {Lerp_to_bg_btn (-bg [min_btn_bg_num].GetComponent<RectTransform> ().anchoredPosition.x);}
		if (temp_bg != min_btn_bg_num) {bg [temp_bg].interactable = false;bg [min_btn_bg_num].interactable = true;temp_bg = min_btn_bg_num;}
	}

	void music_control()
	{
		for (int i = 0; i < music.Length; i++) 
		{
			disResposition_music [i] = center.GetComponent<RectTransform> ().position.x - music [i].GetComponent<RectTransform> ().position.x;
			distance_music [i] = Mathf.Abs (disResposition_music [i]);
			if (disResposition_music [i] > (1500*panel_width)) 
			{
				float curX_music = music [i].GetComponent<RectTransform> ().anchoredPosition.x;
				float curY_music = music [i].GetComponent<RectTransform> ().anchoredPosition.y;
				Vector2 newAnchoredPos_music = new Vector2 (curX_music + (btn_music_lenght * btn_music_distance), curY_music);
				music [i].GetComponent<RectTransform> ().anchoredPosition = newAnchoredPos_music;
			}
			if (disResposition_music [i] < -(1500*panel_width)) {
				float curX_music = music [i].GetComponent<RectTransform> ().anchoredPosition.x;
				float curY_music = music [i].GetComponent<RectTransform> ().anchoredPosition.y;
				Vector2 newAnchoredPos_music = new Vector2 (curX_music - (btn_music_lenght * btn_music_distance), curY_music);
				music [i].GetComponent<RectTransform> ().anchoredPosition = newAnchoredPos_music;
			}
		}
		float min_music_distance = Mathf.Min (distance_music);  //get the min dis
		for (int i = 0; i < music.Length; i++) {if (min_music_distance == distance_music [i]) {	min_btn_music_num = i;}}
		musicNumber = min_btn_music_num;
		if (!dragging) {Lerp_to_music_btn (-music [min_btn_music_num].GetComponent<RectTransform> ().anchoredPosition.x);}
		if (temp_music != min_btn_music_num) {music [temp_music].interactable = false;music [min_btn_music_num].interactable = true;temp_music = min_btn_music_num;}
	}
	void Lerp_to_meun_btn(float position)
	{
		float newX_meun = Mathf.Lerp (select_btn.anchoredPosition.x, position, Time.deltaTime * 5f);
		Vector2 newPosition_meun = new Vector2 (newX_meun, select_btn.anchoredPosition.y);
		select_btn.anchoredPosition = newPosition_meun;
	}
	void Lerp_to_skin_btn(float position)
	{
		float newX_skin = Mathf.Lerp (select_skin.anchoredPosition.x, position, Time.deltaTime * 5f);
		Vector2 newPosition_skin = new Vector2 (newX_skin, select_skin.anchoredPosition.y);
		select_skin.anchoredPosition = newPosition_skin;
	}
	void Lerp_to_bg_btn(float position)
	{
		float newX_bg = Mathf.Lerp (select_bg.anchoredPosition.x, position, Time.deltaTime * 5f);
		Vector2 newPosition_bg = new Vector2 (newX_bg, select_bg.anchoredPosition.y);
		select_bg.anchoredPosition = newPosition_bg;
	}
	void Lerp_to_music_btn(float position)
	{
		float newX_music = Mathf.Lerp (select_music.anchoredPosition.x, position, Time.deltaTime * 5f);
		Vector2 newPosition_music = new Vector2 (newX_music, select_music.anchoredPosition.y);
		select_music.anchoredPosition = newPosition_music;
	}
	public void StartDrag(){dragging = true;}
	public void EndDrag(){dragging = false;}
	public void choose()
	{
		clickSound.Play ();
		if (panel_num == 1) 
		{
			switch (min_btn_menu_num)
			{
			case 0:
				if (Score.life > 0)
				{
					meun_ani.SetBool ("open", false);
					StartCoroutine (delay_meun ());
					if (backGroundNumber == 0){SceneManager.LoadScene (1);} 
					else if (backGroundNumber == 1 && HaveBackground1 == true ) {SceneManager.LoadScene (2);} 
					else if (backGroundNumber == 2 && HaveBackground2 == true ) {SceneManager.LoadScene (3);} 
					else if (backGroundNumber == 3 && HaveBackground3 == true ) {SceneManager.LoadScene (4);}
				} else if (Score.life <= 0) {NoLifeUI.SetActive (true);noLife_isOpen = true;}
				break;
			case 1:
				meun_ani.SetBool ("open", false);
				StartCoroutine (delay_meun ());
				skin_panel.SetActive (true);
				skin_ani.SetBool ("open", true);
				panel_num = 2;
				break;
			case 2:
				meun_ani.SetBool ("open", false);
				StartCoroutine (delay_meun ());
				bg_panel.SetActive (true);
				bg_ani.SetBool ("open", true);
				panel_num = 3;
				break;
			case 3:
				meun_ani.SetBool ("open", false);
				StartCoroutine (delay_meun ());
				music_panel.SetActive (true);
				music_ani.SetBool ("open", true);
				panel_num = 4;
				break;
			case 4:
				StartCoroutine (delay_rank_true());
				Panel.SetActive (true);
				Rank.SetActive (true);
				rank_isOpen = true;
				break;
			case 5:
				if (!bt_ready) {
					KeyboConnectInterface.Initialize (() => {
						KeyboConnectInterface.Log ("Init Success");
						init_success = true;
						StartCoroutine (connect ());
					}, (error) => {KeyboConnectInterface.Log (error);});}
				StartCoroutine(changeMode());
				break;
			case 6:
				is_click = true;
				if (is_click == true) {Application.OpenURL ("http://www.serafim-tech.com");}
				is_click = false;
				break;
			}
		} else if (panel_num == 2) 
		{
			if (min_btn_skin_num == 0) 
			{
				if (HaveSkin0 == true) 
				{
					grandmaNumber = min_btn_skin_num;
					skin_ani.SetBool ("open", false);
					StartCoroutine (delay_skin ());
					panel_num = 1;
				} else if (HaveSkin0 == false) 	{
					if (CanBuySkin [0] == true) {
						BuyingUI.SetActive (true);
						buying_isOpen = true;
					}else if (CanBuySkin[0] == false)NoMoneyUI.SetActive (true);}
			} else if (min_btn_skin_num == 1) {
				if (HaveSkin1 == true) 
				{
					grandmaNumber = min_btn_skin_num;
					skin_ani.SetBool ("open", false);
					StartCoroutine (delay_skin ());
					panel_num = 1;
				} else if (HaveSkin1 == false) {if (CanBuySkin [1] == true) {
						BuyingUI.SetActive (true);
						buying_isOpen = true;
					}else if (CanBuySkin[1] == false)NoMoneyUI.SetActive (true);}
			} else if (min_btn_skin_num == 2) {
				if (HaveSkin2 == true) 
				{
					grandmaNumber = min_btn_skin_num;
					skin_ani.SetBool ("open", false);
					StartCoroutine (delay_skin ());
					panel_num = 1;
				} else if (HaveSkin2 == false) {if (CanBuySkin [2] == true) {
						BuyingUI.SetActive (true);
						buying_isOpen = true;
					}else if (CanBuySkin[2] == false)NoMoneyUI.SetActive (true);}
			} else if (min_btn_skin_num == 3) {
				if (HaveSkin3 == true) 
				{
					grandmaNumber = min_btn_skin_num;
					skin_ani.SetBool ("open", false);
					StartCoroutine (delay_skin ());
					panel_num = 1;
				} else if (HaveSkin3 == false) {if (CanBuySkin [3] == true) {
						BuyingUI.SetActive (true);
						buying_isOpen = true;
					}else if (CanBuySkin[3] == false)NoMoneyUI.SetActive (true);}
			} else if (min_btn_skin_num == 4) {
				if (HaveSkin4 == true) 
				{
					grandmaNumber = min_btn_skin_num;
					skin_ani.SetBool ("open", false);
					StartCoroutine (delay_skin ());
					panel_num = 1;
				} else if (HaveSkin4 == false) {if (CanBuySkin [4] == true) {
						BuyingUI.SetActive (true);
						buying_isOpen = true;
					}else if (CanBuySkin[4] == false)NoMoneyUI.SetActive (true);}
			}
			PlayerPrefs.SetInt ("SelectSkin", min_btn_skin_num);
		} else if (panel_num == 3) 
		{
			if (min_btn_bg_num == 0) 
			{
				if (HaveBackground0 == true) 
				{
					backGroundNumber = min_btn_bg_num;
					bg_ani.SetBool ("open", false);
					StartCoroutine (delay_bg ());
					panel_num = 1;
				}  else if (HaveBackground0 == false) {	if (CanBuyBackground [0] == true) {
						BuyingUI.SetActive (true);
						buying_isOpen = true;
					}else if (CanBuyBackground[0] == false)NoMoneyUI.SetActive (true);}
			}  else if (min_btn_bg_num == 1) {
				if (HaveBackground1 == true) 
				{
					backGroundNumber = min_btn_bg_num;
					bg_ani.SetBool ("open", false);
					StartCoroutine (delay_bg ());
					panel_num = 1;
				}  else if (HaveBackground1 == false) {if (CanBuyBackground [1] == true) {
						BuyingUI.SetActive (true);
						buying_isOpen = true;
					}else if (CanBuyBackground[1] == false)NoMoneyUI.SetActive (true);}
			}  else if (min_btn_bg_num == 2) {
				if (HaveBackground2 == true) 
				{
					backGroundNumber = min_btn_bg_num;
					bg_ani.SetBool ("open", false);
					StartCoroutine (delay_bg ());
					panel_num = 1;
				}  else if (HaveBackground2 == false){if (CanBuyBackground [2] == true) {
						BuyingUI.SetActive (true);
						buying_isOpen = true;
					}else if (CanBuyBackground[2] == false)NoMoneyUI.SetActive (true);}
			}  else if (min_btn_bg_num == 3) {
				if (HaveBackground3 == true) 
				{
					backGroundNumber = min_btn_bg_num;
					bg_ani.SetBool ("open", false);
					StartCoroutine (delay_bg ());
					panel_num = 1;
				}  else if (HaveBackground3 == false) {if (CanBuyBackground [3] == true) {
						BuyingUI.SetActive (true);
						buying_isOpen = true;
					}else if (CanBuyBackground[3] == false)NoMoneyUI.SetActive (true);}
			}
			PlayerPrefs.SetInt ("SelectBackground", min_btn_bg_num);
		} else if (panel_num == 4) 
		{
			if (min_btn_music_num == 0) 
			{
				if (HaveMusic0 == true) 
				{
					musicNumber = min_btn_music_num;
					music_ani.SetBool ("open", false);
					StartCoroutine (delay_music ());
					panel_num = 1;
				}  else if (HaveMusic0 == false) {	if (CanBuyMusic [0] == true) {
						BuyingUI.SetActive (true);
						buying_isOpen = true;
					}else if (CanBuyMusic[0] == false)NoMoneyUI.SetActive (true);}
			}  else if (min_btn_music_num == 1) {
				if (HaveMusic1 == true) 
				{
					musicNumber = min_btn_music_num;
					music_ani.SetBool ("open", false);
					StartCoroutine (delay_music ());
					panel_num = 1;
				}  else if (HaveMusic1 == false) {if (CanBuyMusic [1] == true) {
						BuyingUI.SetActive (true);
						buying_isOpen = true;
					}else if (CanBuyMusic[1] == false)NoMoneyUI.SetActive (true);}
			}  else if (min_btn_music_num == 2) {
				if (HaveMusic2 == true) 
				{
					musicNumber = min_btn_music_num;
					music_ani.SetBool ("open", false);
					StartCoroutine (delay_music ());
					panel_num = 1;
				}  else if (HaveMusic2 == false) {if (CanBuyMusic [2] == true) {
						BuyingUI.SetActive (true);
						buying_isOpen = true;
					}else if (CanBuyMusic[2] == false)NoMoneyUI.SetActive (true);}
				}  else if (min_btn_music_num == 3) {
				if (HaveMusic3 == true) 
				{
					musicNumber = min_btn_music_num;
					music_ani.SetBool ("open", false);
					StartCoroutine (delay_music ());
					panel_num = 1;
				}  else if (HaveMusic3 == false) {if (CanBuyMusic [3] == true) {
						BuyingUI.SetActive (true);
						buying_isOpen = true;
					}else if (CanBuyMusic[3] == false)NoMoneyUI.SetActive (true);}
			}  else if (min_btn_music_num == 4) {
				if (HaveMusic4 == true) 
				{
					musicNumber = min_btn_music_num;
					music_ani.SetBool ("open", false);
					StartCoroutine (delay_music ());
					panel_num = 1;
				}
				else if (HaveMusic4 == false) {if (CanBuyMusic [4] == true) {BuyingUI.SetActive (true);buying_isOpen = true;}
				else if (CanBuyMusic[4] == false)NoMoneyUI.SetActive (true);}
			}
			PlayerPrefs.SetInt ("SelectMusic", min_btn_music_num);
		}
	}
	IEnumerator delay_meun() { yield return new WaitForSeconds(1.15f);menu_panel.SetActive (false);}
	IEnumerator delay_skin() { yield return new WaitForSeconds(1.15f);skin_panel.SetActive (false);menu_panel.SetActive (true);}
	IEnumerator delay_bg() { yield return new WaitForSeconds(1.15f);bg_panel.SetActive (false);menu_panel.SetActive (true);}
	IEnumerator delay_music() { yield return new WaitForSeconds(1.15f);music_panel.SetActive (false);menu_panel.SetActive (true);}
	IEnumerator delay_rank_true() { yield return new WaitForSeconds(0.15f);RankUI.SetActive (true);}
	IEnumerator delay_rank_false() { yield return new WaitForSeconds(0.15f);RankUI.SetActive (false);}

	IEnumerator connect()
	{
		yield return new WaitUntil(() => init_success = true);
		connect_state.GetComponent<Text> ().text = "connecting...";
		ConnectController.connect ();
	}

	private void connectStateChange(string state)
	{
		if(state == "Ready"){bt_ready = true;connect_state.GetComponent<Text>().text = "BT Ready";StartCoroutine(changeMode());}
		else if(state == "Disconnected"){bt_ready = false;connect_state.GetComponent<Text>().text = "BT Disconnected";Keybo_link = false;}
	}
	private void connectError(string error){bt_ready = false;connect_state.GetComponent<Text>().text = "Connect BT Fail";}

	IEnumerator changeMode()
	{
		yield return new WaitUntil(() => bt_ready = true );
		yield return new WaitForSeconds(1.15f);
		//change the keybo mode to coordinate
		KeyboConnectInterface.ChangeKeyboMode (KeyboConnectInterface.KeyboType.Coordinate, (error) => {	
			connect_state.GetComponent<Text>().text = "Click Again!";KeyboConnectInterface.Log (error);	});
		ConnectController.switchTap (true);
		ConnectController.switchPan (true);
		ConnectController.switchSwipe (true);
		connect_state.GetComponent<Text> ().text = "OK!";
		Keybo_link = true;
	}

	public void OnTap(int state,int type,float x,float y){

		if(isPan == false && state == 2 && type == 0 && noLife_isOpen==false && buying_isOpen == false){
			clickSound.Play ();
			if(rank_isOpen == false)
			{
				choose();
			}else if (rank_isOpen == true) 
			{
				StartCoroutine (delay_rank_false());
				rank_isOpen = false;
			}
		}
	}

	public void OnPan(int state,float x,float y)
	{
		if (noLife_isOpen == false && buying_isOpen == false) {
			if (state == 0) {
				isPan = false;
				StartDrag ();
			} else if (state == 1) {
				isPan = true;
				if (panel_num == 1) {
					select_btn.anchoredPosition = new Vector2 (select_btn.anchoredPosition.x + (x * 0.5f), select_btn.anchoredPosition.y);
				} else if (panel_num == 2) {
					select_skin.anchoredPosition = new Vector2 (select_skin.anchoredPosition.x + (x * 0.5f), select_skin.anchoredPosition.y);
				} else if (panel_num == 3) {
					select_bg.anchoredPosition = new Vector2 (select_bg.anchoredPosition.x + (x * 0.5f), select_bg.anchoredPosition.y);
				} else if (panel_num == 4) {
					select_music.anchoredPosition = new Vector2 (select_music.anchoredPosition.x + (x * 0.5f), select_music.anchoredPosition.y);
				}
			} else if (state == 2) {
				EndDrag ();
			}
		}
	}

	public void OnSwipe(int type)
	{
		clickSound.Play ();
		if(type == 2)
		{
			if (buying_isOpen == true) {
				BuyingUI.SetActive (false);
				buying_isOpen = false;
			} else if (noLife_isOpen == true) {
				NoLifeUI.SetActive (false);
				noLife_isOpen = false;
			}
			QuestionUI.SetActive(false);
		}
		else if(type == 3)
		{
			if (buying_isOpen == true) {
				Buy ();
				BuyingUI.SetActive (false);
				buying_isOpen = false;
			}else if (noLife_isOpen == true) {
				Buy ();
				NoLifeUI.SetActive (false);
				noLife_isOpen = false;
			}
		}QuestionUI.SetActive(false);
	}
	void OnApplicationPause()
	{
		is_pause = true;
		//change the keybo mode to coordinate
		KeyboConnectInterface.ChangeKeyboMode (KeyboConnectInterface.KeyboType.Keyboard, (error) => {connect_state.GetComponent<Text>().text = "Change Mode Fail";KeyboConnectInterface.Log (error);});
	}

	public void Buy()
	{
		if (panel_num == 2)
		{
			if (min_btn_skin_num == 1) 
			{
				Score.money = Score.money - SkinCost [1];
				SkinUI [1].SetActive (false);
				skin_img[1].sprite = skin_img_unlock[1];
				HaveSkin1 = true;
				PlayerPrefs.SetInt ("HaveSkin1", 1);
			} else if (min_btn_skin_num == 2) 
			{
				Score.money = Score.money - SkinCost [2];
				SkinUI [2].SetActive (false);
				skin_img[2].sprite = skin_img_unlock[2];
				HaveSkin2 = true;
				PlayerPrefs.SetInt ("HaveSkin2", 1);
			} else if (min_btn_skin_num == 3) 
			{
				Score.money = Score.money - SkinCost [3];
				SkinUI [3].SetActive (false);
				skin_img[3].sprite = skin_img_unlock[3];
				HaveSkin3 = true;
				PlayerPrefs.SetInt ("HaveSkin3", 1);
			} else if (min_btn_skin_num == 4) 
			{
				Score.money = Score.money - SkinCost [4];
				SkinUI [4].SetActive (false);
				skin_img[4].sprite = skin_img_unlock[4];
				HaveSkin4 = true;
				PlayerPrefs.SetInt ("HaveSkin4", 1);
			}
		} else if (panel_num == 3) 
		{
			if (min_btn_bg_num == 1)
			{
				Score.money = Score.money - BackgroundCost [1];
				BackgroundUI [1].SetActive (false);
				bg_img[1].sprite = bg_img_unlock[1];
				HaveBackground1 = true;
				PlayerPrefs.SetInt ("HaveBackground1", 1);
			} else if (min_btn_bg_num == 2) 
			{
				Score.money = Score.money - BackgroundCost [2];
				BackgroundUI [2].SetActive (false);
				bg_img[2].sprite = bg_img_unlock[2];
				HaveBackground2 = true;
				PlayerPrefs.SetInt ("HaveBackground2", 1);
			} else if (min_btn_bg_num == 3)
			{
				Score.money = Score.money - BackgroundCost [3];
				BackgroundUI [3].SetActive (false);
				bg_img[3].sprite = bg_img_unlock[3];
				HaveBackground3 = true;
				PlayerPrefs.SetInt ("HaveBackground3", 1);
			}
		} else if (panel_num == 4) 
		{
			if (min_btn_music_num == 1) 
			{
				Score.money = Score.money - MusicCost [1];
				MusicUI [1].SetActive (false);
				music_img[1].sprite = music_img_unlock[1];
				HaveMusic1 = true;
				PlayerPrefs.SetInt ("HaveMusic1", 1);
			} else if (min_btn_music_num == 2) 
			{
				Score.money = Score.money - MusicCost [2];
				MusicUI [2].SetActive (false);
				music_img[2].sprite = music_img_unlock[2];
				HaveMusic2 = true;
				PlayerPrefs.SetInt ("HaveMusic2", 1);
			} else if (min_btn_music_num == 3) 
			{
				Score.money = Score.money - MusicCost [3];
				MusicUI [3].SetActive (false);
				music_img[3].sprite = music_img_unlock[3];
				HaveMusic3 = true;
				PlayerPrefs.SetInt ("HaveMusic3", 1);
			} else if (min_btn_music_num == 4) 
			{
				Score.money = Score.money - MusicCost [4];
				MusicUI [4].SetActive (false);
				music_img[4].sprite = music_img_unlock[4];
				HaveMusic4 = true;
				PlayerPrefs.SetInt ("HaveMusic4", 1);
			}
		} else if (panel_num == 1) 
		{
			if (CanBuyLife == true) {Score.money = Score.money - LifeCost;Score.life = Score.life + 10;	NoLifeUI.SetActive (false);} 	
			else if (CanBuyLife == false) {NoMoneyUI.SetActive (true);NoLifeUI.SetActive (false);}
		}
	}
}
