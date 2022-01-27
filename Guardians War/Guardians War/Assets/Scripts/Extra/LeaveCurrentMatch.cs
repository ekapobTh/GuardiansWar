using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveCurrentMatch : MonoBehaviour {
	
	public void OnClick_LeaveMatch(){
		PhotonNetwork.LeaveRoom ();
		PlayerNetwork.Instance.joinRoomNum = 0;
		PlayerNetwork.Instance.PlayersInGame = 0;
		PhotonNetwork.LoadLevel (2);
	}
}
