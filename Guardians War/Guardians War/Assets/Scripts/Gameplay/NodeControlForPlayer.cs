using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeControlForPlayer : MonoBehaviour {

	public static NodeControlForPlayer Instance;
	public GameObject playerNodeK1;
	public GameObject playerNodeK2;
	public GameObject playerNodeM1;
	public GameObject playerNodeM2;

	// Use this for initialization
	void Start () {
		Instance = this;
		/*playerNodeK1.SetActive (false);
		playerNodeK2.SetActive (false);
		playerNodeM1.SetActive (false);
		playerNodeM2.SetActive (false);*/
	}

	// Update is called once per frame
	void Update () {
		
	}
}
