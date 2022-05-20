using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeIn : MonoBehaviour
{

	public CanvasGroup canvasGroup;

	// Update is called once per frame
	void Update () 
	{
		if (canvasGroup.alpha > 0)canvasGroup.alpha = canvasGroup.alpha - Time.deltaTime*2;
	}
}
