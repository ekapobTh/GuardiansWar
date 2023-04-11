using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkUnit : MonoBehaviour {
	
	[SerializeField] 
	private MonoBehaviour[] playerControlScripts;

	private PhotonView photonView;
	// Use this for initialization
	void Start () {
		photonView = GetComponent<PhotonView> ();

		Initialize ();
	}
	
	private void Initialize()
	{
		if (photonView.isMine) {

		} else {
			//disable cam

			//disable script
			foreach (MonoBehaviour m in playerControlScripts) {
				m.enabled = false;
			}
		}
	}
}
