using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartAnimation : MonoBehaviour {
	
	public RawImage rImg;
	public Texture2D[] frames;
	public int fps = 10;
	int frameSize;
	Scene m_Scene;
	bool runframe = true;
	private float timer;

	void Start () {
		//timer = 0f;
		m_Scene = SceneManager.GetActiveScene ();
	}
	// Update is called once per frame
	void Update () {
		//Debug.Log (timer);
		//timer += Time.deltaTime;
		int index = (int)(Time.time * fps) % frames.Length;
		if (!runframe)
			index = 109;
		rImg.texture = frames [index];
		if (index == 109 && m_Scene.name == "SplashScreen") {
			FirstSceneScript.Instance.moveScene ();
			runframe = false;
		}

	}
}
