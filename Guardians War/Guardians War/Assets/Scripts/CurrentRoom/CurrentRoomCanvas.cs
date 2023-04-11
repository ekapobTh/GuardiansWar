using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour {
	
	public static CurrentRoomCanvas Instance;
	public bool startStat;
	public GameObject countdown;
	private PhotonView PhotonView;
	[SerializeField]
	private GameObject playerDuration;
	[SerializeField]
	private GameObject hostDuration;
	private float roomPlayerDuration;
	private float roomHostDuration;

	void OnEnable () {
		startStat = false;
		Instance = this;
		countdown.SetActive (false);
		playerDuration.SetActive (false);
		hostDuration.SetActive (false);
		roomPlayerDuration = 0f;
		roomHostDuration = 0f;
	}


	void Update () {
		if (roomPlayerDuration > 0f){
			playerDuration.SetActive (true);
			roomPlayerDuration -= Time.deltaTime;
			if (roomPlayerDuration <= 0f) {
				playerDuration.SetActive (false);
			}
		}
		else if (roomHostDuration > 0f){
			hostDuration.SetActive (true);
			roomHostDuration -= Time.deltaTime;
			if (roomHostDuration <= 0f) {
				hostDuration.SetActive (false);
			}
		}
	}

	//Start and remove room in lobby
	public void OnClickStartDelayed(){		
		if (PhotonNetwork.playerList.Length == 2 || PhotonNetwork.playerList.Length == 4) {
			if (!PhotonNetwork.isMasterClient) {
				roomHostDuration = 3f;
				hostDuration.SetActive (true);
				return;
			}
			startStat = true;
			countdown.SetActive (true);
			//PhotonView.RPC ("RPC_Countdown", PhotonTargets.Others);
		} else {
			roomPlayerDuration = 3f;
			playerDuration.SetActive (true);
		}
	}
}
