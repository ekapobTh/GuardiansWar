using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class FirstSceneScript : MonoBehaviour {

	public static FirstSceneScript Instance;
	bool move = false;
	public RawImage moveImg;

	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown)
			SceneManager.LoadScene (1);
		if (move)
			moveImg.gameObject.transform.Translate (0, 0.25f, 0);
			
	}
	public IEnumerator W()
	{
		move = true;
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene (1);
	}

	public void moveScene(){
		StartCoroutine (W ());
	}
}
