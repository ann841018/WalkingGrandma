using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class ConnectController
{
	public static Action<string> connectReturn;
	public static Action<string> connectError;
	public static Action<int,float,float> panGestureAction;
	public static Action<int,int,float,float> tapGestureAction;
	public static Action<int> swipeGestureAction;

//	public delegate void connectReturn(string state);
	public static void connect(){
		KeyboConnectInterface.Log ("Controller Connect");
		KeyboConnectInterface.KeyboConnect ((state) => {
			KeyboConnectInterface.Log ("Controller ConnectChanged");
			if(connectReturn != null)
				connectReturn(state);
		}, (error) => {
			KeyboConnectInterface.Log ("Controller ConnectError");
			if(connectError != null)
				connectError(error);
		});
	}

	public static void switchTap(Boolean b){
		KeyboConnectInterface.SwitchTapGestureRecognizer (b, (s, t, x, y) => {
			if(tapGestureAction != null)
				tapGestureAction(s,t,x,y);
		});
	}

	public static void switchPan(Boolean b){
		KeyboConnectInterface.SwitchPanGestureRecognizer (b, (s, x, y) => {
			if(panGestureAction != null)
				panGestureAction(s,x,y);
		});
	}
	public static void switchSwipe(Boolean b){
		KeyboConnectInterface.SwitchSwipeGestureRecognizer (b, (s) => {
			if(swipeGestureAction != null)
				swipeGestureAction(s);
		});
	}

}

