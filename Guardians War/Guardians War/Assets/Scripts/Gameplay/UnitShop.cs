using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitShop : MonoBehaviour {

	public static UnitShop Instance;
	public Texture2D[] knightPic;
	public Texture2D[] monsterPic;
	public RawImage[] shopImg;
	public Text[] unitLvl;
	public int[] lvlUnit;
	public Color maxUnitFontColor;
	public int[] lvlPrice;
	public Text unit1price;
	public Text unit2price;
	public Text unit3price;
	public Text unit4price;


	// Use this for initialization
	void Start () {
		unit1price.text = lvlPrice [0].ToString();
		unit2price.text = lvlPrice [0].ToString();
		unit3price.text = lvlPrice [0].ToString();
		unit4price.text = lvlPrice [0].ToString();
		Instance = this;
		if (MotherScript.Instance.currentGameSide == 1 || MotherScript.Instance.currentGameSide == 3) {
			shopImg [0].texture = knightPic [0];
			shopImg [1].texture = knightPic [0];
			shopImg [2].texture = knightPic [0];
			shopImg [3].texture = knightPic [1];
		} else {
			shopImg [0].texture = monsterPic [0];
			shopImg [1].texture = monsterPic [0];
			shopImg [2].texture = monsterPic [0];
			shopImg [3].texture = monsterPic [1];
		}
	}

	public void OnClickUpgradePos1(){
		if (PlayerStats.Money >= lvlPrice [lvlUnit [0]-1]) {
			PlayerStats.Money -= lvlPrice [lvlUnit [0]-1];
			if (lvlUnit [0] < 3) {
				unit1price.text = lvlPrice [1].ToString ();
				CanvasGameplayControl.Instance.upGrade1but.gameObject.SetActive (false);
				CameraController.Instance.UpgradeUnit (1, MotherScript.Instance.currentGameSide);
				lvlUnit [0]++;
				if (lvlUnit [0] == 3)
					unitLvl [0].color = maxUnitFontColor;
				unitLvl [0].text = lvlUnit [0].ToString ();
			}
		} else {
			PauseAndExitButton.Instance.RunNoGold ();
		}
	}
	public void OnClickUpgradePos2(){
		if (PlayerStats.Money >= lvlPrice [lvlUnit [1]-1]) {
			PlayerStats.Money -= lvlPrice [lvlUnit [1]-1];
			if (lvlUnit [1] < 3) {
				unit2price.text = lvlPrice [1].ToString();
				CanvasGameplayControl.Instance.upGrade2but.gameObject.SetActive (false);
				CameraController.Instance.UpgradeUnit (2, MotherScript.Instance.currentGameSide);
				lvlUnit [1]++;
				if (lvlUnit [1] == 3)
					unitLvl [1].color = maxUnitFontColor;
				unitLvl [1].text = lvlUnit [1].ToString ();
			}
		} else {
			PauseAndExitButton.Instance.RunNoGold ();
		}
	}
	public void OnClickUpgradePos3(){
		if (PlayerStats.Money >= lvlPrice [lvlUnit [2]-1]) {
			PlayerStats.Money -= lvlPrice [lvlUnit [2] - 1];
			if (lvlUnit [2] < 3) {
				unit3price.text = lvlPrice [1].ToString ();
				CanvasGameplayControl.Instance.upGrade3but.gameObject.SetActive (false);
				CameraController.Instance.UpgradeUnit (3, MotherScript.Instance.currentGameSide);
				lvlUnit [2]++;
				if (lvlUnit [2] == 3)
					unitLvl [2].color = maxUnitFontColor;
				unitLvl [2].text = lvlUnit [2].ToString ();
			}
		} else {
			PauseAndExitButton.Instance.RunNoGold ();
		}
	}
	public void OnClickUpgradePos4(){
		if (PlayerStats.Money >= lvlPrice [lvlUnit [3]-1]) {
			PlayerStats.Money -= lvlPrice [lvlUnit [3]-1];
			if (lvlUnit [3] < 3) {
				unit4price.text = lvlPrice [1].ToString();
				CanvasGameplayControl.Instance.upGrade4but.gameObject.SetActive (false);
				CameraController.Instance.UpgradeUnit (4, MotherScript.Instance.currentGameSide);
				lvlUnit [3]++;
				if (lvlUnit [3] == 3) 
					unitLvl [3].color = maxUnitFontColor;
				unitLvl [3].text = lvlUnit [3].ToString ();
			}
		} else {
			PauseAndExitButton.Instance.RunNoGold ();
		}
	}



}
