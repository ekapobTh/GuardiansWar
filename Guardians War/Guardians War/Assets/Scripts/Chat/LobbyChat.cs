using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon.Chat;
using UnityEngine.UI;

public class LobbyChat : MonoBehaviour , IChatClientListener {

	public static LobbyChat Instance;
	public ChatClient chatClient;
	public Text channelChat;
	public InputField msgInput;
	public Text msgArea;
	// Use this for initialization\

	void Start () {
		Instance = this;
		Application.runInBackground = true;
		if (string.IsNullOrEmpty (PhotonNetwork.PhotonServerSettings.ChatAppID)) {
			print ("Chat : No chat ID provided");
			return;
		}
	}

	void OnEnable()
	{
		getConnected ();
	}
		

	void OnGUI() {
		if (Event.current.Equals (Event.KeyboardEvent ("return"))) {
			msgInput.Select ();
			sendMsg ();
		}
	}

	void Update(){
		if (this.chatClient != null) {
			this.chatClient.Service ();
		}
	}

	public void getConnected(){
		print ("Chat : Trying to connect");
		chatClient = new ChatClient (this);
		chatClient.Connect (PhotonNetwork.PhotonServerSettings.ChatAppID, "anything", new ExitGames.Client.Photon.Chat.AuthenticationValues (PlayerNetwork.Instance.PlayerName));
	}

	public void OnConnected() 
	{
		Debug.Log ("Chat : Connected " + channelChat.text);
		chatClient.Subscribe(new string[] {channelChat.text});
		chatClient.SetOnlineStatus (ChatUserStatus.Online);
	}

	public void sendMsg()
	{
		if (msgInput.text != "") {
			chatClient.PublishMessage (channelChat.text, msgInput.text);
			msgInput.Select ();
		}
 		msgInput.text = "";
	}

	public void OnDisconnected()
	{
		msgArea.text = "";
		gameObject.SetActive (false);
	}

	public void OnGetMessages (string channelName,string[] senders,object[] messages) 
	{
		for (int i = 0; i < senders.Length; i++) {
			msgArea.text += senders [i] + " : " + messages[i] + "\n";
		}
	}

	public void OnPrivateMessage(string sender,object  message,string  channelName)
	{}

	public void OnSubscribed(string[] channels, bool[] results)
	{
		this.chatClient.PublishMessage (channelChat.text, "Joined");
	}

	public void OnUnsubscribed(string[] channels)
	{}

	public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
	{
	}



	public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message)
	{}

	public void OnChatStateChange(ChatState state)
	{}
}
