using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameplayControl : MonoBehaviour {

	public static CanvasGameplayControl Instance;
	public GameObject gameMaster;
	public GameObject standbyCam;
	private int allPlayerInGame;
	public GameObject loadingImg;
	public bool winStat = false;
	public Text countDownWave;
	public Text playerGold;
	public GameObject shopUI;
	public GameObject unitUI;
	public Button openShopBut;
	public Button CloseShopBut;
	public Button openUnitBut;
	public Button CloseUnitBut;
	public Button upGrade1but;
	public Button upGrade2but;
	public Button upGrade3but;
	public Button upGrade4but;

	// Use this for initialization
	void Start () {
		Instance = this;
		allPlayerInGame = PhotonNetwork.playerList.Length;
		standbyCam.SetActive (true);
		playerGold.text = PlayerStats.Money.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		playerGold.text = PlayerStats.Money.ToString ();
		if (PhotonNetwork.connectionStateDetailed.ToString () == "Joined") {
			standbyCam.SetActive (false);
		}

		if (allPlayerInGame != PhotonNetwork.playerList.Length && !winStat) {
			PauseAndExitButton.Instance.pause = true;
			PauseAndExitButton.Instance.allButton.SetActive (true);
			PauseAndExitButton.Instance.exitButton.SetActive (false);
			CanvasGameplayControl.Instance.winStat = true;
			PlayerStats.Instance.endGameStat = true;
		}
	}

	public void OnClickOpenShop(){
		shopUI.SetActive (true);
		CloseShopBut.gameObject.SetActive (true);
		openShopBut.gameObject.SetActive (false);
	}

	public void OnClickCloseShop(){
		shopUI.SetActive (false);
		openShopBut.gameObject.SetActive (true);
		CloseShopBut.gameObject.SetActive (false);
	}
	public void OnClickOpenUnit(){
		unitUI.SetActive (true);
		CloseUnitBut.gameObject.SetActive (true);
		openUnitBut.gameObject.SetActive (false);
	}

	public void OnClickCloseUnit(){
		unitUI.SetActive (false);
		CloseUnitBut.gameObject.SetActive (false);
		openUnitBut.gameObject.SetActive (true);
		upGrade1but.gameObject.SetActive (false);
		upGrade2but.gameObject.SetActive (false);
		upGrade3but.gameObject.SetActive (false);
		upGrade4but.gameObject.SetActive (false);
	}

	public void OnClickAllowUpgrade1(){
		if(UnitShop.Instance.lvlUnit[0] < 3)
			upGrade1but.gameObject.SetActive (true);
	}
	public void OnClickAllowUpgrade2(){
		if(UnitShop.Instance.lvlUnit[1] < 3)
		upGrade2but.gameObject.SetActive (true);
	}
	public void OnClickAllowUpgrade3(){
		if(UnitShop.Instance.lvlUnit[2] < 3)
		upGrade3but.gameObject.SetActive (true);
	}
	public void OnClickAllowUpgrade4(){
		if(UnitShop.Instance.lvlUnit[3] < 3)
		upGrade4but.gameObject.SetActive (true);
	}

	public void OnClickExitUpgrade1(){
		upGrade1but.gameObject.SetActive (false);
	}
	public void OnClickExitUpgrade2(){
		upGrade2but.gameObject.SetActive (false);
	}
	public void OnClickExitUpgrade3(){
		upGrade3but.gameObject.SetActive (false);
	}
	public void OnClickExitUpgrade4(){
		upGrade4but.gameObject.SetActive (false);
	}
}
