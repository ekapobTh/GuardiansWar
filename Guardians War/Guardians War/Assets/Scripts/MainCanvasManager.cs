using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasManager : MonoBehaviour {

	public static MainCanvasManager Instance;
	public GameObject lobbychatObj;
	public GameObject connectPanel;

	[SerializeField]
	private GameObject lobbyCanvasObj;

	[SerializeField]
	private GameObject currentRoomCanvasObj;

	[SerializeField]
	private LobbyCanvas _lobbyCanvas;
	public LobbyCanvas LobbyCanvas
	{
		get { return _lobbyCanvas; }
	}

	[SerializeField]
	private CurrentRoomCanvas _currentRoomCanvas;
	public CurrentRoomCanvas CurrentRoomCanvas
	{
		get{ return _currentRoomCanvas; }
	}

	[SerializeField]
	private LobbyChat _lobbyChat;
	public LobbyChat LobbyChat
	{
		get{ return _lobbyChat; }
	}

	private void Awake(){
		connectPanel.SetActive (true);
		lobbyCanvasObj.SetActive (true);
		currentRoomCanvasObj.SetActive (true);
		lobbychatObj.SetActive (false);
		Instance = this;
	}

}
