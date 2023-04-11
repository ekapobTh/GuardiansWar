using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogCanvasScript : MonoBehaviour {
	public GameObject battleLog;
	public GameObject logPos;
	// Use this for initialization
	public void OnClickShowBattleLog(){
		battleLog.transform.position = logPos.transform.position;
	}
}
