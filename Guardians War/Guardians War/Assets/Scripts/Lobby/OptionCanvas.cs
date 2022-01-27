using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionCanvas : MonoBehaviour {

	public InputField playerName;

	void Start () {
		playerName.text = PlayerNetwork.Instance.PlayerName;
	}
	public void OnClickBackToLobby(){
		gameObject.SetActive (false);
	}

	public void OnClickSave(){
		MotherScript.Instance.inGameName = playerName.text;
		PlayerNetwork.Instance.changeStat = true;
		Debug.Log (MotherScript.Instance.inGameName);
		MotherScript.Instance.Save ();
	}
}
