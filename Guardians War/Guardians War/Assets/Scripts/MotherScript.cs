using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MotherScript : MonoBehaviour {

	public static MotherScript Instance;
	public string inGameName;
	public float soundMusic;
	public float soundSfx;
	// Use this for initialization
	void Start () {
		Instance = this;
		DontDestroyOnLoad (this);
		StartCoroutine(W());
		Load ();
		if (inGameName == "") {
			inGameName = "Distul#" + Random.Range (1000, 9999);
			Save ();
		}
		Debug.Log (inGameName);
	}
	Encryptor enc = new Encryptor();
	public void Save()
	{
		Saveclass savedata = new Saveclass();
		savedata.inGameName = inGameName;
		savedata.soundMusic = soundMusic;
		savedata.soundSfx = soundSfx;
		File.WriteAllText(Application.persistentDataPath + "/GuardiansWar.txt", enc.Encrypt(JsonUtility.ToJson(savedata), "Keyword"));
	}
	IEnumerator W()
	{
		yield return new WaitForSeconds (1);

		SceneManager.LoadScene (1);
	}
	public void Load()
	{
		Saveclass managerscript = new Saveclass();
		if (File.Exists(Application.persistentDataPath + "/GuardiansWar.txt"))
		{
			string text = File.ReadAllText(Application.persistentDataPath + "/GuardiansWar.txt");
			managerscript = JsonUtility.FromJson<Saveclass>(enc.Decrypt(text, "Keyword"));
			inGameName = managerscript.inGameName;
			soundMusic = managerscript.soundMusic;
			soundSfx = managerscript.soundSfx;
		}
	}
}
public class Saveclass 
{
	public string inGameName;
	public float soundMusic;
	public float soundSfx;
}