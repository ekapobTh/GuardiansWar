using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Turret : Photon.MonoBehaviour {

	private Transform target;
	[SerializeField]
	private int turretCost;
	private PhotonView PhotonView;
	public bool UseTransformView = true;
	private Vector3 TargetPosition;
	private Quaternion TargetRotation;
	public int onNode;
	public GameObject turretUI;
	public GameObject upGradePrefab;
	public Text costToUpgradetxt;
	public Text costToSelltxt;
	public Button upgradeButton;

	[Header("General")]

	public float range = 15f;

	[Header("Use Bullets(default)")]
	public GameObject bulletPrefab;
	public float fireRate = 1f;
	public bool canSlow = false;
	private float fireCountdown = 0f;

	[Header("Use Laser")]
	public bool useLaser = false;

	public int damageOverTime = 30;
	public float slowAmount = 0.5f;

	public LineRenderer lineRenderer;
	public ParticleSystem impactEffect;
	public Light impactLight;

	[Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";

	public Transform partToRotate;
	public float turnSpeed = 10f;

	public Transform firePoint;

	// Use this for initialization
	void Start () {
		if (upGradePrefab == null) {
			upgradeButton.interactable = false;
			costToUpgradetxt.text = "MAX"; 
		} else {
			Turret upPrefScript = upGradePrefab.GetComponent<Turret> ();
			costToUpgradetxt.text = upPrefScript.GetCost ().ToString (); 
		}
		costToSelltxt.text = (turretCost / 3).ToString();
		turretUI.SetActive (false);
		PhotonView = GetComponent<PhotonView> ();
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget ()
	{
		if (!PlayerStats.Instance.endGameStat) {
			if (photonView.isMine) {
				GameObject[] enemies = GameObject.FindGameObjectsWithTag (enemyTag);
				float shortestDistance = Mathf.Infinity;
				GameObject nearestEnemy = null;
				foreach (GameObject enemy in enemies) {
					float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
					if (distanceToEnemy < shortestDistance) {
						shortestDistance = distanceToEnemy;
						nearestEnemy = enemy;
					}
				}

				if (nearestEnemy != null && shortestDistance <= range) {
					target = nearestEnemy.transform;
				} else {
					target = null;
				}
			} else {

				SmoothMove ();
			}
		}

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			HideUI ();
		}

		if (!PlayerStats.Instance.endGameStat) {
			if (photonView.isMine) {
				if (target == null) {
					if (useLaser) {
						if (lineRenderer.enabled) {
							lineRenderer.enabled = false;
							impactEffect.Stop ();
							impactLight.enabled = false;
						}
					}

					return;
				}

				LockOnTarget ();

				if (useLaser) {
					Laser ();
				} else {
					if (fireCountdown <= 0f) {
						Shoot ();
						fireCountdown = 1f / fireRate;
					}

					fireCountdown -= Time.deltaTime;
				}
			} else {
				SmoothMove ();
			}
		}
	}

	void LockOnTarget()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);
	}

	void Laser()
	{
		target.GetComponent<Enemy>().TakeDamage(damageOverTime * Time.deltaTime);
		target.GetComponent<Enemy> ().Slow(slowAmount);

		if (!lineRenderer.enabled) 
		{
			lineRenderer.enabled = true;
			impactEffect.Play();
			impactLight.enabled = true;
		}

		lineRenderer.SetPosition (0, firePoint.position);
		lineRenderer.SetPosition (1, target.position);

		Vector3 dir = firePoint.position - target.position;

		impactEffect.transform.position = target.position + dir.normalized;

		impactEffect.transform.rotation = Quaternion.LookRotation(dir);
	}

	void Shoot ()
	{
		GameObject bulletGO = PhotonNetwork.Instantiate (Path.Combine ("Prefabs/GameUnit", bulletPrefab.name), firePoint.position, firePoint.rotation, 0);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if(canSlow)
			target.GetComponent<Enemy> ().Slow(slowAmount);

		if (bullet != null)
			bullet.Seek(target);
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}

	private void SmoothMove(){
		if (UseTransformView) {
			return;
		}
		transform.position = Vector3.Lerp (transform.position, TargetPosition, 0.25f);
		partToRotate.rotation = Quaternion.RotateTowards (partToRotate.rotation, TargetRotation, 500 * Time.deltaTime);
	}
	private void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (UseTransformView) {
			return;
		}
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (partToRotate.rotation);
		} else {
			TargetPosition = (Vector3)stream.ReceiveNext ();
			TargetRotation = (Quaternion)stream.ReceiveNext ();
		}
	}

	public void OnClickSell(){
		if (photonView.isMine) {
			PlayerStats.Money += turretCost / 3;
			if (MotherScript.Instance.currentGameSide == 1) {
				TestNode node = TestNode1.Instance.node [onNode];
				node.SetNodeToNull ();
			} else if (MotherScript.Instance.currentGameSide == 2) {
				TestNode node = TestNode2.Instance.node [onNode];
				node.SetNodeToNull ();
			} else if (MotherScript.Instance.currentGameSide == 3) {
				TestNode node = TestNode3.Instance.node [onNode];
				node.SetNodeToNull ();
			} else {
				TestNode node = TestNode4.Instance.node [onNode];
				node.SetNodeToNull ();
			}
			PhotonNetwork.Destroy (gameObject);
		}
	}

	public void ShowUI(){
		turretUI.SetActive (true);
	}

	public void HideUI(){
		turretUI.SetActive (false);
	}

	public void OnClickUpgrade(){
		Turret upPrefScript = upGradePrefab.GetComponent<Turret> ();
		if (PlayerStats.Money >= upPrefScript.GetCost()) {
			PlayerStats.Money -= upPrefScript.GetCost ();
			GameObject objTurret = PhotonNetwork.Instantiate (Path.Combine ("Prefabs", upGradePrefab.name), transform.position, transform.rotation, 0);	
			Turret objScript = objTurret.GetComponent<Turret> ();
			objScript.SetOnNode (onNode);
			if (MotherScript.Instance.currentGameSide == 1) {	
				TestNode1.Instance.node [onNode].SetTurret (objTurret, objScript);
			} else if (MotherScript.Instance.currentGameSide == 2) {	
				TestNode2.Instance.node [onNode].SetTurret (objTurret, objScript);
			} else if (MotherScript.Instance.currentGameSide == 3) {	
				TestNode3.Instance.node [onNode].SetTurret (objTurret, objScript);
			} else {
				TestNode4.Instance.node [onNode].SetTurret (objTurret, objScript);
			}
			PhotonNetwork.Destroy (gameObject);
		} else {
			PauseAndExitButton.Instance.RunNoGold ();
		}

	}
	public void OnClickCloseCanvas(){
		turretUI.gameObject.SetActive (false);
	}

	public void SetOnNode(int nodePlace){
		onNode = nodePlace;
	}

	public int GetCost(){
		return turretCost;
	}
}