using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour {

	public static CreateRoom Instance;
	public InputField inputFieldName;
	[SerializeField]
	private Text _roomName;
	public Text RoomName
	{
		get { return _roomName; }
	}

	void Awake(){
		Instance = this;
	}

	public void OnClick_CreateRoom(){
		if (RoomName.text != "") {
			RoomOptions roomOptions = new RoomOptions () { IsVisible = true, IsOpen = true, MaxPlayers = 4 };
			if (PhotonNetwork.CreateRoom (RoomName.text, roomOptions, TypedLobby.Default)) {
				print ("create room successfully sent.");
			} else {
				print ("create room failed to send");
			}
			inputFieldName.Select ();
			inputFieldName.text = "";
			MainCanvasManager.Instance.lobbychatObj.SetActive (true);	//commendout
		} else {
			LobbyCanvas.Instance.roomNameDuration = 3f;
		}
	}

	private void OnPhotonCreateRoomFailed(object[] codeAndMessage){
		print ("create room failed:" + codeAndMessage[1]);
	}

	private void OnCreatedRoom(){
		print ("create room successfully");
	}
		
}

