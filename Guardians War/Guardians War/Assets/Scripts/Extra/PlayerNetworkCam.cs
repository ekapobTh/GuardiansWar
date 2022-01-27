using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkCam : MonoBehaviour {

	[SerializeField]
	private Transform playerCamera;
	[SerializeField] 
	private MonoBehaviour[] playerControlScripts;

	private PhotonView photonView;

	private void Start(){
		photonView = GetComponent<PhotonView> ();

		Initialize ();
	}


	private void Initialize()
	{
		if (photonView.isMine) {
			
		} else {
			//disable cam
			playerCamera.gameObject.SetActive (false);

			//disable script
			foreach (MonoBehaviour m in playerControlScripts) {
				m.enabled = false;
			}
		}
	}
}
