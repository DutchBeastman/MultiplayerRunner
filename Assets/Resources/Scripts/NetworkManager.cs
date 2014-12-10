using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	private Rect ipBox = new Rect(0,Screen.height / 1.5f,Screen.width , Screen.height);
	public Texture ipTexture;
	public string stringToEdit = "Hello World";
	private const string typeName = "Fabian'sRunnerGameName";
	private const string gameName = "Fabian'sRunnerGame";
	private HostData[] hostList;
	public Vector2 guiScreen = new Vector2(100,100);

	private void StartServer()
	{
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName, "A MP runner");

	}
	void ConnectToServer() {
		Network.Connect(stringToEdit, 25000);
	}
	void OnServerInitialized()
	{

		SpawnPlayer();
		Debug.Log("Server Initializied");
	}


	
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer()
	{
		SpawnPlayer();

	}
	private void SpawnPlayer()
	{
		Network.Instantiate(Resources.Load("Prefabs/Player"), new Vector3(5f, 10f, 0f), Quaternion.identity, 0);
	}
	void OnGUI()
	{
		CodeProfiler.Begin("NetworkManager:OnGui");
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3((float)(Screen.width) / (1024f) , (float)(Screen.height) / (720f), 1));		if (!Network.isClient && !Network.isServer)
		{

			if (GUI.Button(new Rect(guiScreen.x, guiScreen.y , guiScreen.x,guiScreen.y), "Start Server")){
				StartServer();
			}
			
			if (GUI.Button(new Rect(guiScreen.x, guiScreen.y * 2,  guiScreen.x,guiScreen.y), "Refresh Hosts")){
				RefreshHostList();
			}

			if(GUI.Button(new Rect(guiScreen.x, guiScreen.y * 3 , guiScreen.x,guiScreen.y), "Connect")){
				ConnectToServer();
			}
			stringToEdit = GUI.TextField(new Rect(guiScreen.x, guiScreen.y * 4,  guiScreen.x,guiScreen.y), stringToEdit, 25);
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);
				}
			}
		}
		CodeProfiler.End("NetworkManager:OnGui");
	}
}
