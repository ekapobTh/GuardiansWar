using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealUnit : MonoBehaviour {

	private Transform target;

	public float range = 15f;
	public string enemyTag = "Enemy";

	public GameObject bulletPrefab;
	public float fireRate = 1f;
	private float fireCountdown = 0f;

	void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
		} else
		{
			target = null;
		}

	}

	void Update () {

		if (fireCountdown <= 0f) {
			Heal ();
			fireCountdown = 1f / fireRate;
		}

			fireCountdown -= Time.deltaTime;
	}

	void Heal ()
	{
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, target.position, target.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if (bullet != null)
			bullet.Seek(target);
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
