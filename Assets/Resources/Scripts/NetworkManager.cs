using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private const string typeName = "Fabian'sRunnerGameName";
	private const string gameName = "Fabian'sRunnerGame";
	private HostData[] hostList;
	
	private void StartServer()
	{
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName, "A MP runner");
		//MasterServer.ipAddress = "127.0.0.1";
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
		Debug.Log("Server Joined");
	}
	private void SpawnPlayer()
	{
		Network.Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
	}
	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				StartServer();
			
			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
				RefreshHostList();
			
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);
				}
			}
		}
	}
}
