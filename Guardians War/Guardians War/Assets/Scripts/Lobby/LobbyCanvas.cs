using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour {

	public static LobbyCanvas Instance;
	[SerializeField]
	private GameObject nameDuration;
	public float roomNameDuration;
	[SerializeField]
	private GameObject blockClick;

	[SerializeField]
	private GameObject optionRect;

	[SerializeField]
	private RoomLayoutGroup _roomLayoutGroup;
	private RoomLayoutGroup RoomLayoutGroup
	{
		get{ return _roomLayoutGroup; }
	}

	private void Awake(){
		Instance = this;
		nameDuration.SetActive (false);
		blockClick.SetActive (false);
		optionRect.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (roomNameDuration > 0f){
			nameDuration.SetActive (true);
			roomNameDuration -= Time.deltaTime;
			if (roomNameDuration <= 0f) {
				nameDuration.SetActive (false);
			}
		}
	}

	public void OnClickJoinRoom(string roomName){
		if (PhotonNetwork.JoinRoom (roomName)) {
		
		} else {
			print ("Join room failed.");
		}
	}

	public void OnClickToOption(){
		optionRect.SetActive (true);
	}

	public void OnClickExit(){
		blockClick.SetActive (true);
	}

	public void OnClickExitYes(){
		PhotonNetwork.Disconnect ();
		LobbyChat.Instance.chatClient.Disconnect ();
		Debug.Log ("Disconnected");
		Application.Quit ();
	}

	public void OnClickExitNo(){
		blockClick.SetActive (false);
	}
}
