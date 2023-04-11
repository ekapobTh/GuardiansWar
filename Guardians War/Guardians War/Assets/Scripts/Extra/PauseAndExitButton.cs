using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAndExitButton : MonoBehaviour {

	public static PauseAndExitButton Instance;
	public GameObject camSpawnPoint;
	public GameObject allButton;
	public GameObject exitButton;
	public bool pause;
	public GameObject notEnoughGold;
	// Use this for initialization
	void Start () {
		Instance = this;
		pause = false;
		notEnoughGold.SetActive (false);
	}

	public void OnClickPauseSelect(){
		allButton.SetActive (true);
		pause = true;
	}

	public void OnClickExitPause(){
		allButton.SetActive (false);
		pause = false;
	}

	IEnumerator NoGold(){
		notEnoughGold.SetActive (true);
		yield return new WaitForSeconds (3f);
		notEnoughGold.SetActive (false);

	}

	public void RunNoGold(){
		StartCoroutine (NoGold ());
	}
}
