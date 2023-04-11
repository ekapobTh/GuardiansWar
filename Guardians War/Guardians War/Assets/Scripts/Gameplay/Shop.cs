using UnityEngine;

public class Shop : MonoBehaviour {

	public TurretBlueprint stdTurret;
	public TurretBlueprint misTurret;
	public TurretBlueprint lasTurret;

	Manager buildManager;

	void Start ()
	{
		buildManager = Manager.instance;
	}
		
	public void SelectStdTurret()
	{
		if (PlayerStats.Money >= 100) {
			buildManager.SelectTurretToBuild (stdTurret, "Turret");
			CurrentTowerPicked.Instance.turretPicShow.texture = CurrentTowerPicked.Instance.turretPic [0];
		}
		else
			PauseAndExitButton.Instance.RunNoGold ();
	}

	public void SelectMisTurret()
	{
		if (PlayerStats.Money >= 500) {
			buildManager.SelectTurretToBuild (misTurret, "MissileLauncher");
			CurrentTowerPicked.Instance.turretPicShow.texture = CurrentTowerPicked.Instance.turretPic [1];
		}
		else
			PauseAndExitButton.Instance.RunNoGold ();
	}
	public void SelectLasTurret()
	{
		if (PlayerStats.Money >= 225) {
			buildManager.SelectTurretToBuild (lasTurret, "LaserBeamer");
			CurrentTowerPicked.Instance.turretPicShow.texture = CurrentTowerPicked.Instance.turretPic [2];
		}
		else
			PauseAndExitButton.Instance.RunNoGold ();
	}
}
