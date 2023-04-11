using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLogTxt : MonoBehaviour {

	// Use this for initialization
	public static BattleLogTxt Instance;
	public Text[] textLog;

	void Start () {
		Instance = this;	

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetAvtiveLog(){
		gameObject.SetActive (true);
	}

	public void SetTxt(int i,string word){
		textLog [i].text = word;
	}
}
