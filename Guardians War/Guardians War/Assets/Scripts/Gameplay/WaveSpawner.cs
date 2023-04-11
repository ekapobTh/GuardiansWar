using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public Transform enemy1Prefab;
	public Transform enemy2Prefab;

	public Transform spawnPoint;

	public float timeBetweenWaves = 5f;
	private float countdown = 2f;

	public Text waveCountdownText;

	private int waveIndex = 0;

	void Update()
	{
		if (countdown <= 0f) 
		{
			StartCoroutine (SpawnWave ());
			countdown = timeBetweenWaves;
		}

		countdown -= Time.deltaTime;

		waveCountdownText.text = Mathf.Round (countdown).ToString ();
	}

	IEnumerator SpawnWave()
	{
		waveIndex++;

		//for (int i = 0; i < waveIndex; i++) 
		//{
			SpawnEnemy1();
			yield return new WaitForSeconds (0.5f);
			SpawnEnemy1();
			yield return new WaitForSeconds (0.5f);
			SpawnEnemy2();
			yield return new WaitForSeconds (0.5f);
		//}
	}

	void SpawnEnemy1()
	{
		Instantiate (enemy1Prefab, spawnPoint.position, spawnPoint.rotation);
	}

	void SpawnEnemy2()
	{
		Instantiate (enemy2Prefab, spawnPoint.position, spawnPoint.rotation);
	}
}
