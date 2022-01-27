using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCanvasScript : MonoBehaviour {
	
	public static SelectCanvasScript Instance;
	[SerializeField]
	private Text clockCount;
	public float timer = 10f;
	private int timeToStr = 10;

	public List<int> mode = new List<int> ();
	public List<int> side = new List<int> ();

	void Awake () {
		mode [0] = 0;
		mode [1] = 0;
		mode [2] = 0;
		side [0] = 0;
		side [1] = 0;
		side [2] = 0;
		Instance = this;
		clockCount.text = timeToStr.ToString ();	
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0f) {
			TimerText ();
		}
	}

	private void TimerText(){
		timer -= Time.deltaTime;
		if (timer > 9f && timer < 10f) {
			timeToStr = 9;
		}
		if (timer > 8f && timer < 9f) {
			timeToStr = 8;
		}
		if (timer > 7f && timer < 8f) {
			timeToStr = 7;
		}
		if (timer > 6f && timer < 7f) {
			timeToStr = 6;
		}
		if (timer > 5f && timer < 6f) {
			timeToStr = 5;
		}
		if (timer > 4f && timer < 5f) {
			timeToStr = 4;
		}
		if (timer > 3f && timer < 4f) {
			timeToStr = 3;
		}
		if (timer > 2f && timer < 3f) {
			timeToStr = 2;
		}
		if (timer > 1f && timer < 2f) {
			timeToStr = 1;
		}
		if (timer < 1f) {
			timeToStr = 0;
		}
		clockCount.text = timeToStr.ToString ();
	}
}
