using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSoulChooseScript : Photon.MonoBehaviour {

	public static PlayerSoulChooseScript Instance;
	public int mode;
	private int sidepick = 0;
	[SerializeField]
	private GameObject burn;
	private bool moveAble;
	private bool toGame = false;
	public bool UseTransformView = true;
	private PhotonView PhotonView;
	private Vector3 TargetPosition;
	private Quaternion TargetRotation;
	public bool m_bombStat = false;
	private int side = 0;

	// Use this for initialization
	private void Awake () {
		Instance = this;
		burn.SetActive (m_bombStat);
		moveAble = true;
		PhotonView = GetComponent<PhotonView> ();
		mode = 0;
	}

	// Update is called once per frame
	void Update () {
		if (!CanvasGameButton.Instance.kickKeyLock) {
			if (PhotonView.isMine && !CanvasGameButton.Instance.pause) {
				CheckInput ();
			} else {
				SmoothMove ();
			}
			if (CanvasGameButton.Instance.timer < 0f && moveAble) {
				moveAble = false;
				if (!toGame) {
					if(PhotonNetwork.isMasterClient && CanvasGameButton.Instance.mode1But.IsInteractable()){
						CanvasGameButton.Instance.CalculateMode ();// CALCULATE MODE
					}
					CanvasGameButton.Instance.mode1But.interactable = false;
					CanvasGameButton.Instance.mode2But.interactable = false;
					Invoke ("MoveToSpawner", 2.5f);
					toGame = true;
				} else if (toGame) {
					CanvasGameButton.Instance.loadingPanel.SetActive (true);
					CanvasGameButton.Instance.clockCount.text = mode.ToString();
					PlayerNetwork.Instance.PlayersInGame = 0;
					if (PhotonNetwork.isMasterClient) {
						if(CanvasGameButton.Instance.knightPicked+CanvasGameButton.Instance.monsterPicked != PhotonNetwork.playerList.Length){
							CanvasGameButton.Instance.CalculateSide ();// CALCULATE SIDE
						}
						photonView.RPC ("RPC_SendMode", PhotonTargets.All,CanvasGameButton.Instance.modePick); 
						photonView.RPC ("RPC_SendSide", PhotonTargets.All,CanvasGameButton.Instance.sidePlayer); 
						//Load Mode
						CanvasGameButton.Instance.MasterLoadScene ();
					}
				}
			}
		}
	}
	private void CheckInput(){
		float moveSpeed = 1f; 
		float rotateSpeed = 100f;
		burn.transform.position = transform.position;

		if (moveAble) {
			float vertical = Input.GetAxis ("Vertical");
			float horizontal = Input.GetAxis ("Horizontal");
			transform.position += transform.forward * (vertical * moveSpeed * Time.deltaTime);
			transform.Rotate (new Vector3 (0, horizontal * rotateSpeed * Time.deltaTime, 0));
			if (Input.GetKeyDown (KeyCode.Space)) {
				m_bombStat = true;
			}
		}
		if (m_bombStat) 
		{
			photonView.RPC ("RPC_Bomb", PhotonTargets.All); 
		}
	}

	private void SmoothMove(){
		if (UseTransformView) {
			return;
		}
		transform.position = Vector3.Lerp (transform.position, TargetPosition, 0.25f);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, TargetRotation, 500 * Time.deltaTime);
	}

	void MoveToSpawner(){
		transform.position = CanvasGameButton.Instance.spawner2 [PlayerNetwork.Instance.joinRoomNum-1].transform.position;
		transform.Rotate (new Vector3(0,0,0));
		moveAble = true;
		CanvasGameButton.Instance.timer = 10f;
		CanvasGameButton.Instance.mode1But.gameObject.SetActive (false);
		CanvasGameButton.Instance.mode2But.gameObject.SetActive (false);
		CanvasGameButton.Instance.kniBut.gameObject.SetActive (true);
		CanvasGameButton.Instance.monBut.gameObject.SetActive (true);
		CanvasGameButton.Instance.kniBut3.gameObject.SetActive (true);
		CanvasGameButton.Instance.monBut4.gameObject.SetActive (true);
	}

	//Transfer data
	private void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (UseTransformView) {
			return;
		}
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		} else {
			TargetPosition = (Vector3)stream.ReceiveNext ();
			TargetRotation = (Quaternion)stream.ReceiveNext ();
		}
	}

	public void ClickMode1(){
		photonView.RPC ("RPC_OnClickMode1", PhotonTargets.All); 
	}

	public void ClickMode2(){
		photonView.RPC ("RPC_OnClickMode2", PhotonTargets.All); 
	}

	public void ClickKnight(int side){
		photonView.RPC ("RPC_OnClickKnight", PhotonTargets.MasterClient,PlayerNetwork.Instance.joinRoomNum,side); 
	}

	public void ClickMonster(int side){
		photonView.RPC ("RPC_OnClickMonster", PhotonTargets.MasterClient,PlayerNetwork.Instance.joinRoomNum,side); 
	}

	[PunRPC] 
	private void RPC_Bomb()
	{ 
		burn.SetActive (true); 
	}

	[PunRPC]
	private void RPC_OnClickMode1(){
		CanvasGameButton.Instance.modePrintTxt.text = "Ah Base is on FIRE";
		mode = 1;
		if(CanvasGameButton.Instance.timer > 3f)
			CanvasGameButton.Instance.timer = 3f;
	}

	[PunRPC]
	private void RPC_OnClickMode2(){
		CanvasGameButton.Instance.modePrintTxt.text = "KilL'a BosSSS";
		mode = 2;
		if(CanvasGameButton.Instance.timer > 3f)
			CanvasGameButton.Instance.timer = 3f;
	}

	[PunRPC]
	private void RPC_OnClickKnight(int playerPos,int side){
		CanvasGameButton.Instance.knightPicked++;
		CanvasGameButton.Instance.sidePlayer[playerPos-1] = side;
		photonView.RPC ("RPC_MasterSide", PhotonTargets.All,CanvasGameButton.Instance.knightPicked,CanvasGameButton.Instance.monsterPicked); 
	}

	[PunRPC]
	private void RPC_OnClickMonster(int playerPos,int side){
		CanvasGameButton.Instance.monsterPicked++;
		CanvasGameButton.Instance.sidePlayer[playerPos-1] = side;
		photonView.RPC ("RPC_MasterSide", PhotonTargets.All,CanvasGameButton.Instance.knightPicked,CanvasGameButton.Instance.monsterPicked); 
	}

	[PunRPC]
	private void RPC_MasterSide(int kniPick,int monPick){
		CanvasGameButton.Instance.knightPickedText.text = kniPick.ToString ();
		CanvasGameButton.Instance.monsterPickedText.text = monPick.ToString ();
		if (kniPick > 0) {
			CanvasGameButton.Instance.kniBut.gameObject.SetActive (false);
			if (kniPick >= PhotonNetwork.playerList.Length/2)
				CanvasGameButton.Instance.kniBut3.interactable = false;
		}
		if (monPick > 0) {
			CanvasGameButton.Instance.monBut.gameObject.SetActive (false);
			if (monPick >= PhotonNetwork.playerList.Length/2)
				CanvasGameButton.Instance.monBut4.interactable = false;
		}
	}

	[PunRPC]
	private void RPC_SendMode(int gameMode){
		MotherScript.Instance.currentGameMode = gameMode;
	}
	[PunRPC]
	private void RPC_SendSide(int[] sidePick){
		MotherScript.Instance.currentGameSide = sidePick[PlayerNetwork.Instance.joinRoomNum-1];
	}
}
