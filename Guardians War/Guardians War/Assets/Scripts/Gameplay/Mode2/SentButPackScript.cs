using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentButPackScript : MonoBehaviour {

	public GameObject sentButPack;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			sentButPack.SetActive (false);
		}
	}

	public void OnClickCloseSentBut(){
		sentButPack.SetActive (false);
	}

	public void OnClickOpenSentBut(){
		sentButPack.SetActive (true);
	}

	public void OnClickSentUnit1Lv1(int i){
		if (PlayerStats.Money >= UnitShopMode2.Instance.unit1Price [i]) {
			CameraController.Instance.SendUnit (MotherScript.Instance.currentGameSide, 1, UnitShopMode2.Instance.prefabPath [i]);
			PlayerStats.Money -= UnitShopMode2.Instance.unit1Price [i];
			PlayerStats.Instance.incomeGold += UnitShopMode2.Instance.unit1Price [i] / 10;
			PlayerStats.Instance.PrintIncome ();
			MotherScript.Instance.playerLog [9]++;
		} else {
			PauseAndExitButton.Instance.RunNoGold ();
		}
	}
	public void OnClickSentUnit1Lv2(int i){
		if (PlayerStats.Money >= UnitShopMode2.Instance.unit1Price [i]) {
			CameraController.Instance.SendUnit (MotherScript.Instance.currentGameSide, 1, UnitShopMode2.Instance.prefabPath [i]);
			PlayerStats.Money -= UnitShopMode2.Instance.unit1Price [i];
			PlayerStats.Instance.incomeGold += UnitShopMode2.Instance.unit1Price [i] / 10;
			PlayerStats.Instance.PrintIncome ();
			MotherScript.Instance.playerLog [10]++;
		} else {
			PauseAndExitButton.Instance.RunNoGold ();
		}
	}
	public void OnClickSentUnit1Lv3(int i){
		if (PlayerStats.Money >= UnitShopMode2.Instance.unit1Price [i]) {
			CameraController.Instance.SendUnit (MotherScript.Instance.currentGameSide, 1, UnitShopMode2.Instance.prefabPath [i]);
			PlayerStats.Money -= UnitShopMode2.Instance.unit1Price [i];
			PlayerStats.Instance.incomeGold += UnitShopMode2.Instance.unit1Price [i] / 10;
			PlayerStats.Instance.PrintIncome ();
			MotherScript.Instance.playerLog [11]++;
		} else {
			PauseAndExitButton.Instance.RunNoGold ();
		}
	}

	public void OnClickSentUnit2Lv1(int i){
		if (PlayerStats.Money >= UnitShopMode2.Instance.unit2Price [i]) {
			CameraController.Instance.SendUnit (MotherScript.Instance.currentGameSide, 2, UnitShopMode2.Instance.prefabPath [i]);
			PlayerStats.Money -= UnitShopMode2.Instance.unit2Price [i];
			PlayerStats.Instance.incomeGold += UnitShopMode2.Instance.unit2Price [i] / 10;
			PlayerStats.Instance.PrintIncome ();
			MotherScript.Instance.playerLog [12]++;
		} else {
			PauseAndExitButton.Instance.RunNoGold ();
		}
	}
	public void OnClickSentUnit2Lv2(int i){
		if (PlayerStats.Money >= UnitShopMode2.Instance.unit2Price [i]) {
			CameraController.Instance.SendUnit (MotherScript.Instance.currentGameSide, 2, UnitShopMode2.Instance.prefabPath [i]);
			PlayerStats.Money -= UnitShopMode2.Instance.unit2Price [i];
			PlayerStats.Instance.incomeGold += UnitShopMode2.Instance.unit2Price [i] / 10;
			PlayerStats.Instance.PrintIncome ();
			MotherScript.Instance.playerLog [13]++;
		} else {
			PauseAndExitButton.Instance.RunNoGold ();
		}
	}
	public void OnClickSentUnit2Lv3(int i){
		if (PlayerStats.Money >= UnitShopMode2.Instance.unit2Price [i]) {
			CameraController.Instance.SendUnit (MotherScript.Instance.currentGameSide, 2, UnitShopMode2.Instance.prefabPath [i]);
			PlayerStats.Money -= UnitShopMode2.Instance.unit2Price [i];
			PlayerStats.Instance.incomeGold += UnitShopMode2.Instance.unit2Price [i] / 10;
			PlayerStats.Instance.PrintIncome ();
			MotherScript.Instance.playerLog [14]++;
		} else {
			PauseAndExitButton.Instance.RunNoGold ();
		}
	}
}
