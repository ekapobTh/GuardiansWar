using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBombEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("DestroyEffect", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void DestroyEffect(){
		PhotonNetwork.Destroy (gameObject);
	}
}
