using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManagerEncode : MonoBehaviour {
	public Text txt;
	private Encryptor enc;

	// Use this for initialization
	void Start () {
		enc=new Encryptor();

		string str;
		string path=Application.persistentDataPath+"/";

		if(System.IO.File.Exists(path+"data.dat")){
			txt.text="Have Data File\n";
			str=System.IO.File.ReadAllText(path+"data.dat");
			txt.text+=enc.Decrypt(str,"happy")+"\n";
		}else{
			txt.text="No Data File\n";
			string ret=enc.Encrypt("1-2-3-4-5-6-7-8-9-10-11-12-13-14-15-16-17-18-19-20-21-22-23-24-25-26-27-28-29-30-31-32-33-34-35-36-37-38-39-40","happy");
			System.IO.File.WriteAllText(path+"data.dat",ret);
			txt.text="Write Data File\n";
		}
	}

}
