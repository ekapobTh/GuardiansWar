using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	private Transform target;

	public int damage = 50;
	public float speed = 70f;

	public GameObject impactEffect;

	public void Seek (Transform _target)
	{
		target = _target;
	}

	void Update () {

		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);

	}

	void HitTarget ()
	{
		GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 2f);

		Damage (target);

		Destroy(gameObject);
	}

	void Damage (Transform enemy)
	{
		CoreScript e = enemy.GetComponent<CoreScript> ();

		if(e!=null)
			e.CoreTakeDamage (damage);

	}

	public void SetDmg(int dmgCost){
		damage = dmgCost;
	}
		
}
