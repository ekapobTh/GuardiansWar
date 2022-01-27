using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameButton : MonoBehaviour {

	[Header("Mode Spawner")]
	public List<GameObject> spawner1 = new List<GameObject>();
	[Header("Side Spawner")]
	public List<GameObject> spawner2 = new List<GameObject>();
	public static CanvasGameButton Instance;
	private bool showButtonState;
	[SerializeField]
	private GameObject connectCamera;
	public GameObject allButton;
	public bool pause;
	public Text clockCount;
	public float timer = 10f;
	private int timeToStr = 10;
	public List<int> mode = new List<int> ();// 1 : Random | 2 : Ah Base is on FIRE | 3 : KilL'a BosSSS
	public int[] side;
	[SerializeField]
	private GameObject kickPlayer; 
	private int limitPlayer;
	public bool kickKeyLock;
	public int playMode;// 1 : Ah Base is on FIRE | 2 : KilL'a BosSSS
	public int playSide;// 1 : Knight | 2 : Monster

	// Use this for initialization
	void Awake () {
		side = new int[PhotonNetwork.playerList.Length];
		clockCount.text = timeToStr.ToString ();	
		pause = false;
		allButton.SetActive (false);
		Instance = this;
		showButtonState = false;
		connectCamera.SetActive (true);
		limitPlayer = PhotonNetwork.playerList.Length;
		kickPlayer.SetActive (false);
		kickKeyLock = false;
		playMode = 0;
		playSide = 0;
	}

	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.connectionStateDetailed.ToString () == "Joined") {
			connectCamera.SetActive (false);
		}
		if (timer > 0f) {
			TimerText ();
		}
		if (PhotonNetwork.playerList.Length < limitPlayer) {
			kickPlayer.SetActive (true);
			kickKeyLock = true;
		}
	}

	public void OnClickPauseSelect(){
		allButton.SetActive (true);
		pause = true;
	}

	public void OnClickExitPause(){
		allButton.SetActive (false);
		pause = false;
	}

	public void CalculateMode(){
		if (mode [1] > mode [2]) {
			playMode = 1;
		} else if (mode [1] < mode [2]) {
			playMode = 2;
		} else {
			int randMode = Random.Range (1, 100);
			Debug.Log ("Random : " + randMode);
			if (randMode % 2 == 1) {
				playMode = 1;
			} else {
				playMode = 2;
			}
		}
	}
	public void CalculateSide(){
		int side1 = 0;
		int side2 = 0;
		int side0 = 0;
		for (int i = 0; i < side.Length; i++) {
			if (side [i] == 1) {
				side1++;
			}
			if (side [i] == 2) {
				side2++;
			}
			if (side [i] == 0) {
				side0++;
			}
		}
		Debug.Log ("0 : "+side0+" 1 : "+side1+" 2 : "+side2);
		if (side0 > 0) {
			for (int i = 0; i < side.Length; i++) {
				Debug.Log ("S[i] " + i + " : " + side [i]);
				if (side [i] == 0) {
					if (side1 < limitPlayer/2) {
						side [i] = 1;
						side1++;
					} else if (side2 < limitPlayer/2) {
						side [i] = 2;
						side2++;
					}
				}
			}
		} else {
			Debug.Log ("Move to game scene");
		}
	}

	private void TimerText(){
		timer -= Time.deltaTime;
		timeToStr = (int)timer;
		clockCount.text = timeToStr.ToString ();
	}
}
