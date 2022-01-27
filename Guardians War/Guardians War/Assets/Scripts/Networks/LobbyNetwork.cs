using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour {

	// Use this for initialization
	private void Start () {
		if (!PhotonNetwork.connected) {
			print ("Connecting to server..");
			PhotonNetwork.ConnectUsingSettings ("0.0.0");
		}
	}

	private void OnConnectedToMaster(){
		print ("Connected to master.");
		PhotonNetwork.automaticallySyncScene = false;/////////////////////////////change
		PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;

		PhotonNetwork.JoinLobby (TypedLobby.Default);
	}

	private void OnJoinedLobby(){
		print ("Joined Lobby");
		MainCanvasManager.Instance.connectPanel.SetActive (false);
		if (!PhotonNetwork.inRoom) {
			MainCanvasManager.Instance.CurrentRoomCanvas.transform.SetAsFirstSibling ();
		}
	}
}
