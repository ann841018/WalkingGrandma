using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KeyboTestScript : MonoBehaviour 
{
	public Transform ConnectView;
	public Transform DataView;
	static private KeyboTestScript _bleTestSCript;
	static public int count = 0;

	static public void Show (Transform panel)
	{
		if (_bleTestSCript == null)
		{
			GameObject gameObject = GameObject.Find ("Canvas");
			if (gameObject != null)
				_bleTestSCript = gameObject.GetComponent<KeyboTestScript> ();
		}

		if (_bleTestSCript != null)	{panel.SetAsLastSibling();}
	}
	// Use this for initialization
	void Start () {	Show (ConnectView);}
}
