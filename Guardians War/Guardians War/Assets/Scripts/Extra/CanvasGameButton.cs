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
	public int[] mode = new int[3];// 1 : Random | 2 : Ah Base is on FIRE | 3 : KilL'a BosSSS
	[SerializeField]
	private GameObject kickPlayer; 
	private int limitPlayer;
	public bool kickKeyLock;
	public int playMode;// 1 : Ah Base is on FIRE | 2 : KilL'a BosSSS
	public int playSide;// 1 : Knight | 2 : Monster
	public int[] sidePlayer;
	public bool clickModeState;
	public Text modePrintTxt;
	public Button mode1But;
	public Button mode2But;
	public Button kniBut;
	public Button monBut;
	public Button kniBut3;
	public Button monBut4;
	public Texture2D[] sidePic = new Texture2D[2];
	public RawImage sidePicChoose;
	public Text knightPickedText;
	public Text monsterPickedText;
	public int knightPicked;
	public int monsterPicked;
	public string modeName;
	public int modePick;
	public GameObject loadingPanel;


	// Use this for initialization
	void Awake () {
		loadingPanel.SetActive (false);
		Instance = this;
		modeName = "";
		modePick = 0;
		knightPicked = 0;
		monsterPicked = 0;
		sidePicChoose.gameObject.SetActive (false);
		clickModeState = true;
		sidePlayer = new int[PhotonNetwork.playerList.Length];
		clockCount.text = timeToStr.ToString ();
		pause = false;
		allButton.SetActive (false);
		showButtonState = false;
		connectCamera.SetActive (true);
		limitPlayer = PhotonNetwork.playerList.Length;
		kickPlayer.SetActive (false);
		kickKeyLock = false;
		playMode = 0;
		playSide = 0;
		if (PhotonNetwork.playerList.Length > 2) {
			mode1But.interactable = false;
		}
	}

	// Update is called once per frame
	void Update () {
		Debug.Log ("mode : "+ modeName);
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

	public void MasterLoadScene(){
		PhotonNetwork.LoadLevel (modeName);
	}

	public void OnClickMode1(){
		if(clickModeState && PhotonNetwork.isMasterClient){
			PlayerSoulChooseScript.Instance.ClickMode1 ();
			modeName = "GameplayTest";
			modePick = 1;
			mode1But.interactable = false;
			mode2But.interactable = false;
			clickModeState = false;
		}
	}
	public void OnClickMode2(){
		if (clickModeState && PhotonNetwork.isMasterClient) {
			modeName = "GameplayTest2";
			modePick = 2;
			PlayerSoulChooseScript.Instance.ClickMode2 ();
			mode1But.interactable = false;
			mode2But.interactable = false;
			clickModeState = false;
		}
	}

	public void OnClickMonster(int side){
		sidePicChoose.gameObject.SetActive (true);
		sidePicChoose.texture = sidePic [1];
		kniBut.interactable = false;//kni
		kniBut3.interactable = false;
		monBut.interactable = false;//mon
		monBut4.interactable = false;
		PlayerSoulChooseScript.Instance.ClickMonster (side);

	}

	public void OnClickKnight(int side){
		sidePicChoose.gameObject.SetActive (true);
		sidePicChoose.texture = sidePic [0];
		kniBut.interactable = false;//kni
		kniBut3.interactable = false;
		monBut.interactable = false;//mon
		monBut4.interactable = false;
		PlayerSoulChooseScript.Instance.ClickKnight (side);

	}

	public void CalculateMode(){
		int randMode = Random.Range (1, 100);
		if (PhotonNetwork.playerList.Length < 4) {
			if (randMode % 2 == 1) {
				PlayerSoulChooseScript.Instance.mode = 1;
				PlayerSoulChooseScript.Instance.ClickMode1 ();
				modeName = "GameplayTest";
			} else {
				PlayerSoulChooseScript.Instance.mode = 2;
				PlayerSoulChooseScript.Instance.ClickMode2 ();
				modeName = "GameplayTest2";
			}
		} else {
			modeName = "GameplayTest2";
		}
	}
		

	private void TimerText(){
		timer -= Time.deltaTime;
		timeToStr = (int)timer;
		clockCount.text = timeToStr.ToString ();
	}

	public void CalculateSide(){
		for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
			if (sidePlayer [i] == 0) {
				if (knightPicked < PhotonNetwork.playerList.Length/2) {
					sidePlayer [i] = 1;
					knightPicked++;
				} else if(monsterPicked < PhotonNetwork.playerList.Length/2){
					sidePlayer [i] = 2;
					monsterPicked++;
				}
			}
		}
	}
}
