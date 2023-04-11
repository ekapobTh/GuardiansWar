using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNode : MonoBehaviour {

	public int nodeNo;
	public Color hoverColor;
	[SerializeField]
	private GameObject turret;
	[SerializeField]
	private Turret turretScript;
	private Renderer rend;
	private Color startColor;
	public static TestNode Instance;

	// Use this for initialization
	void Start () {
		Instance = this;
		turret = null;
		rend = GetComponent<Renderer> ();
		startColor = rend.material.color;
	}


	public void OnMouseEnter(){
		if (PlayerStats.Instance.endGameStat)
			return;
		if (!PlayerStats.Instance.endGameStat) {
			if (!CanvasGameplayControl.Instance.winStat) {
				if (turret == null) {
					if (MotherScript.Instance.currentGameSide.ToString () == tag)
						rend.material.color = hoverColor;
				}
			}
		}
	}

	public void OnMouseDown(){
		if (PlayerStats.Instance.endGameStat)
			return;
		if (!PlayerStats.Instance.endGameStat) {
			if (!CanvasGameplayControl.Instance.winStat) {
				if (turret != null) {
					if (MotherScript.Instance.currentGameSide.ToString () == tag) {
						turretScript.turretUI.SetActive (true);
					}
				} else {
					if (Manager.instance.buildName == null) {
						return;
					}
					if (MotherScript.Instance.currentGameSide.ToString () == tag) {
						CameraController.Instance.currentClickNode = nodeNo;
						CameraController.Instance.CreateTower (Manager.instance.buildName);
						Manager.instance.buildName = null;
					}
				}
			}
		}
	}

	public void OnMouseExit(){
		rend.material.color = startColor;
	}

	public void SetTurret(GameObject obj,Turret script){
		turret = obj;
		turretScript = script;
	}

	public void SetNodeToNull(){
		turret = null;
	}

	public Turret GetTurretOnNode(){
		return turretScript;
	}

	public string GetTurretName(){
		if (turret == null) {
			return null;
		}
		return turret.name;
	}

	public int GetNodeNo(){
		return nodeNo;
	}

}