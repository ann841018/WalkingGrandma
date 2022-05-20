using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ConnectViewScript : MonoBehaviour {
	public DataViewScript PanelDataView;
	public Text InitText;
	public Text ConnectText;
	public Text ScanText;
	public Text RetrieveText;
	public Text BatteryText;

	public void OnInitialize(){
		KeyboConnectInterface.Initialize (() => {
			Debug.Log ("Init Success");
			InitText.text = "Init success";
			KeyboConnectInterface.Log ("Init Success");
		}, (error) => {Debug.Log ("Init Fail");InitText.text = error;
			KeyboConnectInterface.Log (error);
		});
//		PanelDataView.Initialize ();
//		KeyboTestScript.Show (PanelDataView.gameObject.transform);
	}

	public void OnConnect(){	
		KeyboConnectInterface.KeyboConnect ((state) => {
			ConnectText.text = state;
		}, (error) => {ConnectText.text = error;});
	}

	public void OnScanIn3(){
		ScanText.text = Convert.ToString("scan");
		KeyboConnectInterface.ScanKeyboDeviceWithTime (3000,(num) => {
			KeyboConnectInterface.Log("ScanGet");
			ScanText.text = Convert.ToString(num);
		}, (error) => {ScanText.text = error;});
	}

	public void OnRetrieve(){
		KeyboConnectInterface.RetrieveKeyboDevice ((state) => {	
			KeyboConnectInterface.Log("Retrieve");
			RetrieveText.text = Convert.ToString(state);
		});
	}
		
	public void OnData()
	{
		PanelDataView.Initialize ();
		KeyboTestScript.Show (PanelDataView.gameObject.transform);
	}

	public void OnBattery(){KeyboConnectInterface.GetBatteryMessage ((num) => {BatteryText.text = Convert.ToString(num);});}
}
