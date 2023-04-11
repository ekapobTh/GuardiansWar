using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerLayoutGroup : MonoBehaviour {

	[SerializeField]
	private GameObject _playerListingPrefabs;
	private GameObject PlayerListingPrefabs{
		get{ return _playerListingPrefabs; }
	}


	[SerializeField]
	private Text roomNameText;

	private List<PlayerListing> _playerListings = new List<PlayerListing>();
	private List<PlayerListing> PlayerListings
	{
		get { return _playerListings; }
	}

	//Called by photon whenever the master client is switched.
	private void OnMasterClientSwitched(PhotonPlayer newMasterClient){
		PhotonNetwork.LeaveRoom ();
	}

	//Called by photon whenever you join a room.
	private void OnJoinedRoom(){
		roomNameText.text = PhotonNetwork.room.Name.ToString ();
		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}
		MainCanvasManager.Instance.LobbyCanvas.transform.SetAsFirstSibling ();


		PhotonPlayer[] photonPlayers = PhotonNetwork.playerList;
		for (int i = 0; i < photonPlayers.Length; i++) {
			PlayerJoinedRoom (photonPlayers [i]);
			PlayerNetwork.Instance.joinRoomNum = i+1;
		}
		MainCanvasManager.Instance.lobbychatObj.SetActive (true);
	}

	//Called by photon when a player joins the room.
	private void OnPhotonPlayerConnected(PhotonPlayer photonPlayer){
		PlayerJoinedRoom (photonPlayer);
	}

	//Called by Photon when async player leaves the rooms 
	private void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer){
		PlayerLeftRoom (photonPlayer);
	}

	//bring player to the room
	private void PlayerJoinedRoom(PhotonPlayer photonPlayer){
		if (photonPlayer == null) {
			return;
		}

		PlayerLeftRoom (photonPlayer);
		GameObject playerListingObj = Instantiate (PlayerListingPrefabs);
		playerListingObj.transform.SetParent (transform, false);

		PlayerListing playerListing = playerListingObj.GetComponent<PlayerListing> ();
		playerListing.ApplyPhotonPlayer (photonPlayer);

		PlayerListings.Add (playerListing);
	}

	//bring player to lobby
	private void PlayerLeftRoom(PhotonPlayer photonPlayer){
		int index = PlayerListings.FindIndex (x => x.PhotonPlayer == photonPlayer);
		if (index != -1) {
			Destroy (PlayerListings [index].gameObject);
			PlayerListings.RemoveAt (index);
		}
	}
	public void OnClickRoomState(){
		if(!PhotonNetwork.isMasterClient){
			return;
		}

		PhotonNetwork.room.IsOpen = !PhotonNetwork.room.IsOpen;
		PhotonNetwork.room.IsVisible = PhotonNetwork.room.IsOpen;
	}

	public void OnClickLeaveRoom(){
		PlayerNetwork.Instance.joinRoomNum = 0;
		LobbyChat.Instance.chatClient.Disconnect ();
		PhotonNetwork.LeaveRoom ();
	}
}
