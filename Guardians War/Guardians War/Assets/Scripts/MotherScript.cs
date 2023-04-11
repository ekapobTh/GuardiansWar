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
	public int currentGameMode;
	public int currentGameSide;
	[Header("Battle Log")]
	public int[] playerLog;
	//0. - tw1 lv.1
	//1. - tw1 lv.2
	//2 - tw1 lv.3
	//3. - tw2 lv.1
	//4. - tw2 lv.2
	//5 - tw2 lv.3
	//6. - tw3 lv.1
	//7. - tw3 lv.2
	//8 - tw3 lv.3
	//9 - lv unit no.1
	//10 - lv unit no.2
	//11 - lv unit no.3
	//12 - lv unit no.4

	// Use this for initialization
	void Start () {
		currentGameMode = 0;
		currentGameSide = 0;
		Instance = this;
		DontDestroyOnLoad (this);
		Load ();
		if (inGameName == "") {
			inGameName = "Distul#" + Random.Range (1000, 9999);
			Save ();
		}
		Debug.Log (Application.persistentDataPath);
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