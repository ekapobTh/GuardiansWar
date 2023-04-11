using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Node : MonoBehaviour {

	public Color hoverColor;
	public Vector3 positionOffset;

	[Header("Optional")]
	public GameObject turret;
	public TurretBlueprint turretBlueprint;
	public bool isUpgraded = false;

	private Renderer rend;
	private Color startColor;

	Manager buildManager;

	void Start()
	{
		
		buildManager = Manager.instance;

		rend = GetComponent<Renderer> ();
		startColor = rend.material.color;
	}

	public Vector3 GetBuildPosition()
	{
		return transform.position + positionOffset;
	}

	void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject ())
			return;

		if (turret != null) 
		{
			Manager.instance.SelectNode (this);
			return;
		}

		if (!buildManager.CanBuild)
			return;
	
		//BuildTurret(Manager.instance.GetTurretToBuild());
	}

	[PunRPC]
	private void RPC_BuildTurret(TurretBlueprint blueprint)
	{
		if(PlayerStats.Money<blueprint.cost)
		{
			Debug.Log ("Not enough gold!");
			return;
		}

		PlayerStats.Money -= blueprint.cost;

		GameObject _turret =  PhotonNetwork.Instantiate (blueprint.prefabs.name, GetBuildPosition (), Quaternion.identity, 0) as GameObject;
		//GameObject _turret = (GameObject)Instantiate (blueprint.prefabs, GetBuildPosition (), Quaternion.identity);
		turret = _turret;

		turretBlueprint = blueprint;

		Debug.Log (PlayerStats.Money);
	}

	public void UpgradeTurret ()
	{
		if (PlayerStats.Money < turretBlueprint.upgradeCost)
		{
			Debug.Log("Not enough gold to upgrade that!");
			return;
		}

		PlayerStats.Money -= turretBlueprint.upgradeCost;

		Destroy(turret);

		GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		isUpgraded = true;

		Debug.Log("Turret upgraded!");
	}

	public void SellTurret()
	{
		PlayerStats.Money += turretBlueprint.GetSellAmount();
		Destroy(turret);
		turretBlueprint = null;
	}

	void OnMouseEnter()
	{
		if (!buildManager.CanBuild)
			return;
		if (tag != MotherScript.Instance.currentGameSide.ToString())
			return;
		rend.material.color = hoverColor;
	}

	void OnMouseExit()
	{
		rend.material.color = startColor;
	}
}
