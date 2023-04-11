using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotherLogScript : MonoBehaviour {

	public static MotherLogScript Instance;
	public BattleLogTxt[] battleLogScript;
	// Use this for initialization
	void Start () {
		Instance = this;
		Debug.Log (PhotonNetwork.playerList.Length);
		for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
			battleLogScript [i].SetAvtiveLog ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
