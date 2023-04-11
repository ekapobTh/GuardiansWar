using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepCoreScript : MonoBehaviour {

	public static KeepCoreScript Instance;
	public CoreScript[] coreScriptKeep; //1 - kni   2 - mon
	// Use this for initialization
	void Start () {
		Instance = this;	
	}
}
