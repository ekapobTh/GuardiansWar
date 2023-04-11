using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {
	public static Countdown Instance;
	public float countnum;
	public GameObject[] showNum;
	private static string count3 = "Game Start in 3";
	private static string count2 = "Game Start in 2";
	private static string count1 = "Game Start in 1";
	private bool stat3;
	private bool stat2;
	private bool stat1;
	// Use this for initialization
	void OnEnable () {
		showNum [0].SetActive (false);
		showNum [1].SetActive (false);
		showNum [2].SetActive (false);
		CurrentRoomCanvas.Instance.startStat = true;
		Instance = this;
		stat3 = true;
		stat2 = true;
		stat1 = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentRoomCanvas.Instance.startStat) {
			countnum = 3;
			CurrentRoomCanvas.Instance.startStat = false;
		}
		//Debug.Log ("Countnum" + countnum);
		if (countnum < 0) {
			/*showNum [0].SetActive (false);
			showNum [1].SetActive (false);
			showNum [2].SetActive (false);*/
		} else {
			countnum -= Time.deltaTime;
			if (countnum < 0) {
				GameStart ();
			}
		}
		if (countnum > 2f && countnum < 3f) {
			if (stat3) {
				stat3 = false;
				LobbyChat.Instance.chatClient.PublishMessage (LobbyChat.Instance.channelChat.text,count3);
			}
			/*showNum [0].SetActive (true);
			showNum [1].SetActive (false);
			showNum [2].SetActive (false);*/
		}
		else if (countnum > 1f && countnum < 2f) {
			if (stat2) {
				stat2 = false;
				LobbyChat.Instance.chatClient.PublishMessage (LobbyChat.Instance.channelChat.text,count2);
			}
			/*showNum [0].SetActive (false);
			showNum [1].SetActive (true);
			showNum [2].SetActive (false);*/
		}
		else if (countnum > 0f && countnum < 1f) {
			if (stat1) {
				stat1 = false;
				LobbyChat.Instance.chatClient.PublishMessage (LobbyChat.Instance.channelChat.text,count1);
			}
			/*showNum [0].SetActive (false);
			showNum [1].SetActive (false);
			showNum [2].SetActive (true);*/
		}
	}

	private void GameStart(){
		if (!PhotonNetwork.isMasterClient) {
			return;
		}
		PhotonNetwork.room.IsOpen = false;
		PhotonNetwork.room.IsVisible = false;
		PhotonNetwork.LoadLevel (3);
	}
}
