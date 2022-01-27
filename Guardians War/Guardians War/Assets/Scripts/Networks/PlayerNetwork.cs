using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class PlayerNetwork : MonoBehaviour {

	public static PlayerNetwork Instance;
	public string PlayerName{ get; private set;}
	private PhotonView PhotonView;
	public int PlayersInGame = 0;
	private ExitGames.Client.Photon.Hashtable m_playerCustomProperties = new ExitGames.Client.Photon.Hashtable ();
	private PlayerMovement CurrentPlayer;
	private Coroutine m_pingCoroutine;
	public int joinRoomNum;
	public bool changeStat;

	// Use this for initialization
	private void Awake () {
		Instance = this;
		PlayersInGame = 0;
		PhotonView = GetComponent<PhotonView> ();
		PlayerName = MotherScript.Instance.inGameName;
		PhotonNetwork.sendRate = 60;
		PhotonNetwork.sendRateOnSerialize = 30;
		SceneManager.sceneLoaded += OnSceneFinishedLoading;
		changeStat = false;
		joinRoomNum = 0;
	}


	//Move player in room to game scene
	private void OnSceneFinishedLoading(Scene scene,LoadSceneMode mode){
		if (scene.name == "Game") {
			if (PhotonNetwork.isMasterClient) {
				MasterLoadedGame ();
			} else {
				NonMasterLoadedGame ();
			}
		}	
	}

	private void Update(){
		Debug.Log ("Status : " + PhotonNetwork.connectionStateDetailed.ToString() + " Player : " + PlayersInGame);
		if (changeStat) {
			PlayerName = MotherScript.Instance.inGameName;
			changeStat = false;
		}


	}

	private void MasterLoadedGame(){
		PhotonView.RPC ("RPC_LoadedGameScene", PhotonTargets.MasterClient,PhotonNetwork.player);
		PhotonView.RPC ("RPC_LoadGameOther", PhotonTargets.Others);
	}

	private void NonMasterLoadedGame(){
		PhotonView.RPC ("RPC_LoadedGameScene", PhotonTargets.MasterClient,PhotonNetwork.player);
	}


	//Bring all player to game 
	[PunRPC]
	private void RPC_LoadGameOther(){
		PhotonNetwork.LoadLevel (3);
	}
		

	[PunRPC]
	private void RPC_LoadedGameScene(PhotonPlayer photonPlayer){
		PlayerManagement.Instance.AddPlayerStats (photonPlayer);
		PlayersInGame++;
		if (PlayersInGame == PhotonNetwork.playerList.Length) {
			print ("All player are in the game scene.");
			PhotonView.RPC ("RPC_CreatePlayer",PhotonTargets.All);
		}
	}

	public void NewHealth(PhotonPlayer photonPlayer,int health){
		PhotonView.RPC ("RPC_NewHealth",photonPlayer,health);
	}

	[PunRPC]
	private void RPC_NewHealth(int health){
		if (CurrentPlayer == null) {
			return;
		}
		if (health <= 0) {
			PhotonNetwork.Destroy (CurrentPlayer.gameObject);
		} else {
			CurrentPlayer.Health = health;
		}
	}

	//Spawn player controlable unit
	[PunRPC]
	private void RPC_CreatePlayer(){
		GameObject obj =  PhotonNetwork.Instantiate (Path.Combine ("Prefabs", "PlayerSoul"), CanvasGameButton.Instance.spawner1[joinRoomNum-1].transform.position, Quaternion.identity, 0);
		CurrentPlayer = obj.GetComponent<PlayerMovement> ();
	}


		
	private IEnumerator C_SetPing(){
		while(PhotonNetwork.connected){
			m_playerCustomProperties ["Ping"] = PhotonNetwork.GetPing ();
			PhotonNetwork.player.SetCustomProperties (m_playerCustomProperties);

			yield return new WaitForSeconds (5f);
		}

		yield break;
	}


	private void OnConnectedToMaster(){
		if (m_pingCoroutine != null) {
			StopCoroutine (m_pingCoroutine);
		}
		m_pingCoroutine = StartCoroutine (C_SetPing ());
	}
}
