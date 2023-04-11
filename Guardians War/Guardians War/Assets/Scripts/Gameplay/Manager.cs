using UnityEngine;

public class Manager : MonoBehaviour {

	public static Manager instance;

	void Awake ()
	{
		if (instance != null) 
		{
			return;
		}
		instance = this;
		buildName = null;
	}

	private TurretBlueprint turretToBuild;
	public string buildName;
	private Node selectedNode;

	public NodeUI nodeUI;

	public GameObject t1MidStart;
	//public GameObject t1MidEnd;
	public GameObject t2MidStart;
	//public GameObject t2MidEnd;

	public bool CanBuild { get { return turretToBuild != null; } }

	public void SelectNode (Node node)
	{
		if (selectedNode == node) 
		{
			DeselectNode ();
			return;
		}

		selectedNode = node;
		turretToBuild = null;

		nodeUI.SetTarget (node);
	}

	public void DeselectNode()
	{
		selectedNode = null;
		nodeUI.Hide ();
	}

	public void SelectTurretToBuild (TurretBlueprint turret,string name)
	{
		turretToBuild = turret;
		buildName = name;
		Debug.Log (name);
		DeselectNode ();
	}

	public TurretBlueprint GetTurretToBuild()
	{
		return turretToBuild;
	}
}
