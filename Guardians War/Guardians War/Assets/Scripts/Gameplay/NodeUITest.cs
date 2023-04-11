using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUITest : MonoBehaviour {

	public static NodeUITest Instance;
	public GameObject editUI;

	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		

	}
	public void OnMouseEnter(){
		Debug.Log ("EIEI");
	}
}
