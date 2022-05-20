using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		//PlayerPrefs.DeleteAll();//清除資料
		if (PlayerPrefs.GetInt ("New") == 0) 
		{
			PlayerPrefs.DeleteAll();//清除資料
			PlayerPrefs.SetInt ("Life", 10);
			PlayerPrefs.SetInt ("Time", 180);
			//Score.money = 10000;//記得拿掉
			PlayerPrefs.SetInt ("HaveSkin1",0);
			PlayerPrefs.SetInt ("HaveSkin2",0);
			PlayerPrefs.SetInt ("HaveSkin3",0);
			PlayerPrefs.SetInt ("HaveSkin4",0);
			PlayerPrefs.SetInt ("HaveBackground1",0);
			PlayerPrefs.SetInt ("HaveBackground2",0);
			PlayerPrefs.SetInt ("HaveBackground3",0);
			PlayerPrefs.SetInt ("HaveMusic1",0);
			PlayerPrefs.SetInt ("HaveMusic2",0);
			PlayerPrefs.SetInt ("HaveMusic3",0);
			PlayerPrefs.SetInt ("HaveMusic4",0);
			PlayerPrefs.SetInt ("SelectSkin", 0);
			PlayerPrefs.SetInt ("SelectBackground", 0);
			PlayerPrefs.SetInt ("SelectMusic", 0);
		}
	}
	
	// Update is called once per frame
	void Update () {PlayerPrefs.SetInt ("New", 1);}
}
