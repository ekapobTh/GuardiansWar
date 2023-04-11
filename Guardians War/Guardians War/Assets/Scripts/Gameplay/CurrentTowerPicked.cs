using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentTowerPicked : MonoBehaviour {

	public static CurrentTowerPicked Instance;
	public Texture2D[] turretPic;
	public RawImage turretPicShow;
	public Color ImgColor;
	public Color startColor;
	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (Manager.instance.buildName != null) {
			turretPicShow.color = ImgColor;
		} else {
			turretPicShow.color = startColor;
		}
	}

	public void OnClickRemovePicked(){
		Manager.instance.buildName = null;
	}
}
