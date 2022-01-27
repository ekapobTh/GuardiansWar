using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credit : MonoBehaviour {

	[SerializeField]
	private Text backWarn;
	// Use this for initialization
	private void Awake(){
		backWarn.gameObject.SetActive (false);
	}

	void Start () {
		Invoke ("ShowWarn", 3f);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			gameObject.SetActive (false);
		}
	}

	private void ShowWarn(){
		backWarn.gameObject.SetActive (true);
	}

}
