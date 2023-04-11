using UnityEngine;
using System.IO;

public class Bullet : Photon.MonoBehaviour {

	private Transform target;
	public bool UseTransformView = true;
	private Vector3 TargetPosition;
	private Quaternion TargetRotation;

	public int damage = 50;

	public float speed = 70f;
	public float explosionRadius = 0f;
	public GameObject impactEffect;

	public void Seek (Transform _target)
	{
		target = _target;
	}

	// Update is called once per frame
	void Update () {

		if (PlayerStats.Instance.endGameStat)
			PhotonNetwork.Destroy (gameObject);

		if (target == null) {
			Destroy (gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;
		if (dir.magnitude <= distanceThisFrame) {
			HitTarget ();
			return;
		}

		if (photonView.isMine) {
			transform.Translate (dir.normalized * distanceThisFrame, Space.World);
			transform.LookAt (target);
		} else {

			SmoothMove ();
		}


	}

	private void SmoothMove(){
		if (UseTransformView) {
			return;
		}
		transform.position = Vector3.Lerp (transform.position, TargetPosition, 0.25f);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, TargetRotation, 500 * Time.deltaTime);
	}
	private void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (UseTransformView) {
			return;
		}
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		} else {
			TargetPosition = (Vector3)stream.ReceiveNext ();
			TargetRotation = (Quaternion)stream.ReceiveNext ();
		}
	}
	void HitTarget ()
	{
		GameObject effectIns = PhotonNetwork.Instantiate (Path.Combine ("Prefabs/GameUnit", impactEffect.name), transform.position, transform.rotation, 0);

		if (explosionRadius > 0f) 
		{
			Explode ();
		} 
		else 
		{
			Damage (target);
		}

		PhotonNetwork.Destroy (gameObject);
	}

		

	void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere (transform.position, explosionRadius);
		foreach (Collider collider in colliders) 
		{
			if (collider.tag == "Enemy") 
			{
				Damage (collider.transform);
			}
		}
	}

	void Damage (Transform enemy)
	{
		Enemy e = enemy.GetComponent<Enemy> ();

		if(e!=null)
			e.TakeDamage (damage);

	}
}