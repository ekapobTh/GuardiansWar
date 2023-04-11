using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class P2Spawner : Photon.MonoBehaviour {

	public static P2Spawner Instance;
	public Transform enemy1Prefab;
	public Transform enemy2Prefab;
	public Transform enemy3Prefab;
	public Transform enemy4Prefab;
	[SerializeField]
	private string[] prefabPath;
	[SerializeField]
	private int[] unitLvl;

	public Transform spawnPoint;

	public float timeBetweenWaves;
	private float countdown = 60f;

	public Text waveCountdownText;

	private int waveIndex = 0;

	void Start()
	{
		if(PhotonNetwork.isMasterClient)
			Instance = this;

		if (MotherScript.Instance.currentGameMode == 1) {
			timeBetweenWaves = 10f;
		} else {
			timeBetweenWaves = 10f;
		}

	}

	void Update()
	{
		if (!PlayerStats.Instance.endGameStat) {
			if (countdown <= 0f) {
				if (PhotonNetwork.isMasterClient && MotherScript.Instance.currentGameMode == 1)
					StartCoroutine (SpawnWave ());
				countdown = timeBetweenWaves;
			}

			countdown -= Time.deltaTime;
		
		
		}
	}

	IEnumerator SpawnWave()
	{
		waveIndex++;
		SpawnEnemy1();
		yield return new WaitForSeconds (0.5f);
		SpawnEnemy2();
		yield return new WaitForSeconds (0.5f);
		SpawnEnemy3();
		yield return new WaitForSeconds (0.5f);
		SpawnEnemy4();
	}

	void SpawnEnemy1()
	{
		GameObject unit = PhotonNetwork.Instantiate (Path.Combine (prefabPath[0], enemy1Prefab.name), spawnPoint.position, spawnPoint.rotation, 0);
	}

	void SpawnEnemy2()
	{
		GameObject unit = PhotonNetwork.Instantiate (Path.Combine (prefabPath[1], enemy2Prefab.name), spawnPoint.position, spawnPoint.rotation, 0);
	}

	void SpawnEnemy3()
	{
		GameObject unit = PhotonNetwork.Instantiate (Path.Combine (prefabPath[2], enemy3Prefab.name), spawnPoint.position, spawnPoint.rotation, 0);
	}

	void SpawnEnemy4()
	{
		GameObject unit = PhotonNetwork.Instantiate (Path.Combine (prefabPath[3], enemy4Prefab.name), spawnPoint.position, spawnPoint.rotation, 0);
	}

	public void UpgradeUnit(int pos){
		Manager.instance.buildName = null;
		if (unitLvl [pos - 1] == 1) {
			prefabPath [pos - 1] += "/lvl2";
			unitLvl [pos - 1]++;
		}
		else if(unitLvl [pos - 1] == 2){
			prefabPath [pos - 1] += "/lvl3";
			unitLvl [pos - 1]++;
		}
	}
	///////mode 2

	public void SpawnEnemy1 (string pathPrefab)
	{
		string path = prefabPath [0] + pathPrefab;
		Debug.Log (path);
		GameObject unit = PhotonNetwork.Instantiate (Path.Combine (path, enemy1Prefab.name), spawnPoint.position, spawnPoint.rotation, 0);
	}
	public void SpawnEnemy2 (string pathPrefab)
	{
		string path = prefabPath [0] + pathPrefab;
		Debug.Log (path);
		GameObject unit = PhotonNetwork.Instantiate (Path.Combine (path, enemy4Prefab.name), spawnPoint.position, spawnPoint.rotation, 0);
	}
}
