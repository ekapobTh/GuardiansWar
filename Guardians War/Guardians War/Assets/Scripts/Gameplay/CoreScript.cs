using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreScript : MonoBehaviour {

	public float coreHealth;
	public float coreCurrentHealth;
	public GameObject winPopUp;
	public Transform winPos;
	public Image coreHealthBar;

	void Start () 
	{
		coreCurrentHealth = coreHealth;
	}

	public void CoreTakeDamage(float amount)
	{
		coreCurrentHealth -= amount;
		coreHealthBar.fillAmount = coreCurrentHealth/coreHealth;
		if (coreCurrentHealth <= 0)
			Die ();
	}

	void Die()
	{
		//CameraController.Instance.MoveToPos (winPos);
		Destroy (gameObject);
		winPopUp.SetActive (true);
		//----------------------------------------------------------
	}

	/*
	void OnTriggerEnter(Collider other) {
		Debug.Log ("Tag : " + other.tag);
		if (other.tag == "Enemy") {
			Enemy dmgScript = other.GetComponent <Enemy>();
			CoreTakeDamage (dmgScript.GetDmg());
			dmgScript.Die ();
		}
	}
	*/

}
