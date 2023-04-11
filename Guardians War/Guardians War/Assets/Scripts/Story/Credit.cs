using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credit : MonoBehaviour {

	public static Credit Instance;
	[SerializeField]
	private Text backWarn;
	// Use this for initialization
	public float timer;
	private void Awake(){
		backWarn.gameObject.SetActive (false);
	}

	void Start () {
		Instance = this;
		timer = 0;
	}

	// Update is called once per frame
	void Update () {
		
		if (timer > 0f) {
			if (timer == 13f)
				Invoke ("ShowWarn", 3f);
			timer -= Time.deltaTime;
		} else {
			gameObject.SetActive (false);
			backWarn.gameObject.SetActive (false);
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			gameObject.SetActive (false);
			backWarn.gameObject.SetActive (false);
		}
	}

	private void ShowWarn(){
		backWarn.gameObject.SetActive (true);
	}

}
