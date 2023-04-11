using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class P4EnemyMovement : Photon.MonoBehaviour {

	private Transform target;
	private int wavepointIndex = 0;
	public bool UseTransformView = true;
	private Vector3 TargetPosition;
	private Quaternion TargetRotation;
	private PhotonView PhotonView;
	[SerializeField]
	private GameObject unitToRotate;
	public bool moveAble;
	private Enemy enemy;

	void Start()
	{
		moveAble = true;
		enemy = GetComponent<Enemy> ();
		PhotonView = GetComponent<PhotonView> ();
		target = P4Waypoints.points[0];
	}
	void Update()
	{
		if (PhotonNetwork.isMasterClient && moveAble) {
			Vector3 dir = target.position - transform.position;
			transform.Translate (dir.normalized * enemy.speed * Time.deltaTime, Space.World);
			LockOnTarget ();

			if (Vector3.Distance (transform.position, target.position) <= 0.2f) {
				GetNextWaypoint ();
			}

			enemy.speed = enemy.startSpeed;
		} else {
			SmoothMove ();
		}
	}

	void GetNextWaypoint()
	{
		if (wavepointIndex == P4Waypoints.points.Length-2) 
		{
			WarpToMidLane ();
		}

		if (wavepointIndex >= P4Waypoints.points.Length-1) 
		{
			return;
		}

		wavepointIndex++;
		target = P4Waypoints.points [wavepointIndex];
	}

	void LockOnTarget()
	{
		Vector3 pos = target.position - unitToRotate.transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(pos);
		Vector3 rotation = Quaternion.Lerp(unitToRotate.transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
		unitToRotate.transform.rotation = Quaternion.Euler (0f, rotation.y, 0f);
	}

	void WarpToMidLane()
	{
		this.transform.position = Manager.instance.t2MidStart.transform.position;
	}
	private void SmoothMove(){
		if (UseTransformView) {
			return;
		}
		transform.position = Vector3.Lerp (transform.position, TargetPosition, 0.25f);
		unitToRotate.transform.rotation = Quaternion.RotateTowards (unitToRotate.transform.rotation, TargetRotation, 500 * Time.deltaTime);
	}
	private void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (UseTransformView) {
			return;
		}
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (unitToRotate.transform.rotation);
		} else {
			TargetPosition = (Vector3)stream.ReceiveNext ();
			TargetRotation = (Quaternion)stream.ReceiveNext ();
		}
	}
	public void DontMove(){
		moveAble = false;
	}
	public void DisableScipt(){
		this.enabled = false;
	}
}
