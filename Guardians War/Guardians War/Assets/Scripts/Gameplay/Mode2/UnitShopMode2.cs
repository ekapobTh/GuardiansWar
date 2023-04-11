using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitShopMode2 : MonoBehaviour {

	public static UnitShopMode2 Instance;
	public Texture2D[] knightPic;
	public Texture2D[] monsterPic;
	public RawImage[] shopImg;
	public string[] prefabPath;
	public int[] unit1Price;
	public int[] unit2Price;
	public Text[] unit1PriceTxt;
	public Text[] unit2PriceTxt;
	// Use this for initialization
	void Start () {
		Instance = this;
		if (MotherScript.Instance.currentGameSide == 1 || MotherScript.Instance.currentGameSide == 3) {
			shopImg [0].texture = knightPic [0];
			shopImg [1].texture = knightPic [1];
		} else {
			shopImg [0].texture = monsterPic [0];
			shopImg [1].texture = monsterPic [1];
		}
		unit1PriceTxt [0].text = unit1Price [0].ToString ();
		unit1PriceTxt [1].text = unit1Price [1].ToString ();
		unit1PriceTxt [2].text = unit1Price [2].ToString ();
		unit2PriceTxt [0].text = unit2Price [0].ToString ();
		unit2PriceTxt [1].text = unit2Price [1].ToString ();
		unit2PriceTxt [2].text = unit2Price [2].ToString ();

	}

}
