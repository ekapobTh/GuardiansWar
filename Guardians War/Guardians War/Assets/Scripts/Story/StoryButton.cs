using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryButton : MonoBehaviour {

	[SerializeField]
	private GameObject credit;
	// Use this for initialization
	void Start () {
		credit.SetActive (false);
	}

	public void OnClickToMainScene(){
		SceneManager.LoadScene (2);
	}

	public void OnClickToCredit(){
		credit.SetActive (true);
	}

	public void OnClickExit(){
		Application.Quit ();
	}
}
