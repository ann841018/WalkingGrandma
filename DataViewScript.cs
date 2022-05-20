using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class DataViewScript : MonoBehaviour {

	public ConnectViewScript PanelConnectView;
	public Text RawText;
	public Text SwipeText;
	public Text PanText;
	public Text TapText;
	public Text PinchText;
	public Text LongPressText;
	public Text RotateText;
	public Text PianoText;
	private float PanV = 5.0f;

	public void Initialize(){}

	public void OnRaw(){KeyboConnectInterface.GetCoordinateRawData ((message) => {RawText.text = message;});}
	public void OnSwipe(){KeyboConnectInterface.SwitchSwipeGestureRecognizer (true, (type) => {SwipeText.text = Convert.ToString(type);});}
	public void OnPan(){KeyboConnectInterface.SwitchPanGestureRecognizer (true, (state,x,y) => {PanText.text = String.Format("{0} x:{1} y:{2}",state,x,y);});}
	public void OnTap(){KeyboConnectInterface.SwitchTapGestureRecognizer (true, (state,type,x,y) => {TapText.text = String.Format("s:{0} t:{1} x:{2} y:{3}",state,type,x,y);});}
	public void OnPinch(){KeyboConnectInterface.SwitchPinchGestureRecognizer (true, (state,x,y,v) => {PinchText.text = String.Format("s:{0} v:{1} x:{2} y:{3}",state,v,x,y);});}
	public void OnLongPress(){KeyboConnectInterface.SwitchLongPressGestureRecognizer (true, (x,y) => {LongPressText.text = String.Format("x:{0} y:{1}",x,y);});}
	public void OnRotate(){KeyboConnectInterface.SwitchRotationGestureRecognizer (true, (state,x,y,v) => {RotateText.text = String.Format("s:{0} v:{1} x:{2} y:{3}",state,v,x,y);});}
	public void OnPiano()
	{
		KeyboConnectInterface.ChangeKeyboMode (KeyboConnectInterface.KeyboType.Piano,(error) =>{KeyboConnectInterface.Log(error);});
		KeyboConnectInterface.GetPianoRawData ((message) => {PianoText.text = message;});
	}
	public void OnKeyboard(){KeyboConnectInterface.ChangeKeyboMode (KeyboConnectInterface.KeyboType.Keyboard,(error) =>{KeyboConnectInterface.Log(error);});}
	public void OnBack(){KeyboTestScript.Show (PanelConnectView.gameObject.transform);}
	public void OnCoordinate(){KeyboConnectInterface.ChangeKeyboMode (KeyboConnectInterface.KeyboType.Coordinate, (error) => {KeyboConnectInterface.Log (error);});}
	public void OnUpPan(){PanV += 1.0f;KeyboConnectInterface.ChangePanGestureVariance (PanV);}
	public void OnDownPan(){if (PanV >= 0) {PanV -= 1.0f;KeyboConnectInterface.ChangePanGestureVariance (PanV);}
	}
}