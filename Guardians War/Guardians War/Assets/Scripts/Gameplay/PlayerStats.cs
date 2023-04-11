using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	public static PlayerStats Instance;
	public Text incomeTxt;
	public bool endGameStat;
	public static int Money;
	public int startMoney = 1000;
	public int incomeGold;
	[Header("Tower 1")]
	public GameObject tw1lv1;
	public GameObject tw1lv2;
	public GameObject tw1lv3;
	[Header("Tower 2")]
	public GameObject tw2lv1;
	public GameObject tw2lv2;
	public GameObject tw2lv3;
	[Header("Tower 3")]
	public GameObject tw3lv1;
	public GameObject tw3lv2;
	public GameObject tw3lv3;


	void Start()
	{
		endGameStat = false;
		Instance = this;
		Money = startMoney;
		incomeGold = startMoney/2;
		PrintIncome ();
	}

	public void PrintIncome(){
		if (incomeTxt != null) {
			incomeTxt.text = incomeGold.ToString ();
		}
	}
}
