using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CameraController : Photon.MonoBehaviour {

	public static CameraController Instance;
	public GameObject thisCam;
	private bool doMovement = true;
	public float panSpeed = 30f;
	public float panBorderThickness = 10f;
	public float scrollSpeed = 5f;
	public float minY = 10f;
	public float maxY = 80f;
	private PhotonView PhotonView;
	public bool UseTransformView = true;
	private Vector3 TargetPosition;
	private Quaternion TargetRotation;
	private GameObject turretToBuildx; // prefab of tower need to add more
	public GameObject standardTurretPrefabx;
	public int currentClickNode;

	void Start (){
		Instance = this;
		PhotonView = GetComponent<PhotonView> ();
		turretToBuildx = standardTurretPrefabx;
	}
	void Update ()
	{
		if (!PauseAndExitButton.Instance.pause && photonView.isMine) {
			MoveCode ();
		}
	}

	private void MoveCode(){
		if (!PlayerStats.Instance.endGameStat) {
			if (Input.GetKeyDown (KeyCode.Escape))
				doMovement = !doMovement;

			if (!doMovement)
				return;

			if (Input.GetKey ("w") || Input.GetKey (KeyCode.UpArrow)/* || Input.mousePosition.y >= Screen.height - panBorderThickness*/) {
				transform.Translate (Vector3.forward * panSpeed * Time.deltaTime, Space.World);
			}
			if (Input.GetKey ("s") || Input.GetKey (KeyCode.DownArrow)/* || Input.mousePosition.y <= panBorderThickness*/) {
				transform.Translate (Vector3.back * panSpeed * Time.deltaTime, Space.World);
			}
			if (Input.GetKey ("d") || Input.GetKey (KeyCode.RightArrow)/* || Input.mousePosition.x >= Screen.width - panBorderThickness*/) {
				transform.Translate (Vector3.right * panSpeed * Time.deltaTime, Space.World);
			}
			if (Input.GetKey ("a") || Input.GetKey (KeyCode.LeftArrow)/* || Input.mousePosition.x <= panBorderThickness*/) {
				transform.Translate (Vector3.left * panSpeed * Time.deltaTime, Space.World);
			}

			float scroll = Input.GetAxis ("Mouse ScrollWheel");

			Vector3 pos = transform.position;

			pos.y += scroll * 1000 * scrollSpeed * Time.deltaTime;
			pos.y = Mathf.Clamp (pos.y, minY, maxY);

			transform.position = pos;
		}
	}

	private void SmoothMove(){
		if (UseTransformView) {
			return;
		}
		transform.position = Vector3.Lerp (transform.position, TargetPosition, 0.25f);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, TargetRotation, 500 * Time.deltaTime);
	}
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

	public void CreateTower(string name){
		if (MotherScript.Instance.currentGameSide == 1) {
			GameObject objTurret = PhotonNetwork.Instantiate (Path.Combine ("Prefabs", Manager.instance.buildName), TestNode1.Instance.node[currentClickNode].transform.position, TestNode2.Instance.node[currentClickNode].transform.rotation, 0);
			Turret objScript = objTurret.GetComponent<Turret> ();
			objScript.SetOnNode (currentClickNode);
			TestNode1.Instance.node [currentClickNode].SetTurret (objTurret, objScript);
			int cost = objScript.GetCost ();
			PlayerStats.Money -= cost;
		}
		else if (MotherScript.Instance.currentGameSide == 2) {
			GameObject objTurret = PhotonNetwork.Instantiate (Path.Combine ("Prefabs", Manager.instance.buildName), TestNode2.Instance.node[currentClickNode].transform.position, TestNode2.Instance.node[currentClickNode].transform.rotation, 0);
			Turret objScript = objTurret.GetComponent<Turret> ();
			TestNode2.Instance.node [currentClickNode].SetTurret (objTurret, objScript);
			objScript.SetOnNode (currentClickNode);
			int cost = objScript.GetCost ();
			PlayerStats.Money -= cost;
		}
		else if (MotherScript.Instance.currentGameSide == 3) {
			GameObject objTurret = PhotonNetwork.Instantiate (Path.Combine ("Prefabs", Manager.instance.buildName), TestNode3.Instance.node[currentClickNode].transform.position, TestNode2.Instance.node[currentClickNode].transform.rotation, 0);
			Turret objScript = objTurret.GetComponent<Turret> ();
			TestNode3.Instance.node [currentClickNode].SetTurret (objTurret, objScript);
			objScript.SetOnNode (currentClickNode);
			int cost = objScript.GetCost ();
			PlayerStats.Money -= cost;
		}
		else if (MotherScript.Instance.currentGameSide == 4) {
			GameObject objTurret = PhotonNetwork.Instantiate (Path.Combine ("Prefabs", Manager.instance.buildName), TestNode4.Instance.node[currentClickNode].transform.position, TestNode2.Instance.node[currentClickNode].transform.rotation, 0);
			Turret objScript = objTurret.GetComponent<Turret> ();
			TestNode4.Instance.node [currentClickNode].SetTurret (objTurret, objScript);
			objScript.SetOnNode (currentClickNode);
			int cost = objScript.GetCost ();
			PlayerStats.Money -= cost;
		}
	}

	public void UpgradeUnit(int pos,int side){
		if (side == 1)
			photonView.RPC ("RPC_Mon1UpgradeUnit", PhotonTargets.MasterClient, pos);
		if (side == 2)
			photonView.RPC ("RPC_Kni2UpgradeUnit", PhotonTargets.MasterClient, pos);
		if (side == 3)
			photonView.RPC ("RPC_Kni4UpgradeUnit", PhotonTargets.MasterClient, pos);
		if (side == 4)
			photonView.RPC ("RPC_Mon3UpgradeUnit", PhotonTargets.MasterClient, pos);
	}

	[PunRPC]
	private void RPC_Kni2UpgradeUnit(int pos){
		P1Spawner.Instance.UpgradeUnit (pos);
	}
	[PunRPC]
	private void RPC_Mon1UpgradeUnit(int pos){
		P2Spawner.Instance.UpgradeUnit (pos);
	}
	[PunRPC]
	private void RPC_Kni4UpgradeUnit(int pos){
		P3Spawner.Instance.UpgradeUnit (pos);
	}
	[PunRPC]
	private void RPC_Mon3UpgradeUnit(int pos){
		P4Spawner.Instance.UpgradeUnit (pos);
	}

	public void MoveToPos(Transform winPos){
		transform.position = winPos.position;
	}

	/////////mode2

	public void SendUnit(int side,int unitPos,string path){
		if (side == 1) {
			if(unitPos == 1)
				photonView.RPC ("RPC_Mon1UpgradeUnit1", PhotonTargets.MasterClient,path);
			if(unitPos == 2)
				photonView.RPC ("RPC_Mon1UpgradeUnit2", PhotonTargets.MasterClient,path);
		}
		if (side == 2) {
			if(unitPos == 1)
				photonView.RPC ("RPC_Kni2UpgradeUnit1", PhotonTargets.MasterClient,path);
			if(unitPos == 2)
				photonView.RPC ("RPC_Kni2UpgradeUnit2", PhotonTargets.MasterClient,path);
		}
		if (side == 3) {
			if(unitPos == 1)
				photonView.RPC ("RPC_Kni4UpgradeUnit1", PhotonTargets.MasterClient,path);
			if(unitPos == 2)
				photonView.RPC ("RPC_Kni4UpgradeUnit2", PhotonTargets.MasterClient,path);
		}
		if (side == 4) {
			if(unitPos == 1)
				photonView.RPC ("RPC_Mon3UpgradeUnit1", PhotonTargets.MasterClient,path);
			if(unitPos == 2)
				photonView.RPC ("RPC_Mon3UpgradeUnit2", PhotonTargets.MasterClient,path);
		}
	}
	[PunRPC]
	private void RPC_Kni2UpgradeUnit1(string pos){
		P1Spawner.Instance.SpawnEnemy1 (pos);
	}
	[PunRPC]
	private void RPC_Kni2UpgradeUnit2(string pos){
		P1Spawner.Instance.SpawnEnemy2 (pos);
	}

	[PunRPC]
	private void RPC_Mon1UpgradeUnit1(string pos){
		P2Spawner.Instance.SpawnEnemy1 (pos);
	}
	[PunRPC]
	private void RPC_Mon1UpgradeUnit2(string pos){
		P2Spawner.Instance.SpawnEnemy2 (pos);
	}

	[PunRPC]
	private void RPC_Kni4UpgradeUnit1(string pos){
		P3Spawner.Instance.SpawnEnemy1 (pos);
	}
	[PunRPC]
	private void RPC_Kni4UpgradeUnit2(string pos){
		P3Spawner.Instance.SpawnEnemy2 (pos);
	}

	[PunRPC]
	private void RPC_Mon3UpgradeUnit1(string pos){
		P4Spawner.Instance.SpawnEnemy1 (pos);
	}
	[PunRPC]
	private void RPC_Mon3UpgradeUnit2(string pos){
		P4Spawner.Instance.SpawnEnemy2 (pos);
	}
}