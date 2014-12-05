
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float smooth = 1000f;
	private Vector2 playerVector;
	private float tempVel;
	public float playerPosX;
	public float playerPosY;

	public float speed = 10f;
	
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = rigidbody.position;
			stream.Serialize(ref syncPosition);
			
			syncVelocity = rigidbody.velocity;
			stream.Serialize(ref syncVelocity);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = rigidbody.position;
		}
	}
	void Awake()
	{
		lastSynchronizationTime = Time.time;
	}

	void FixedUpdate () 
	{
		CodeProfiler.Begin("Player:FixedUpdate");
		playerPosX = transform.position.x;
		playerPosY = transform.position.y;
		if (networkView.isMine)
		{
			PlayerMovement();
			InputColorChange();
		}else
		{
			SyncedMovement();
		}
		//PlayerMovement();
		CodeProfiler.End("Player:FixedUpdate");
	}
	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		
		rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}
	
	
	private void InputColorChange()
	{
		if (Input.GetKeyDown(KeyCode.R))
			ChangeColorTo(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
	}
	
	[RPC] void ChangeColorTo(Vector3 color)
	{
		renderer.material.color = new Color(color.x, color.y, color.z, 1f);
		
		if (networkView.isMine)
			networkView.RPC("ChangeColorTo", RPCMode.OthersBuffered, color);
	}
	void PlayerMovement()
	{
		CodeProfiler.Begin("Player:PlayerMovement");

		rigidbody2D.rotation = 0;
		playerVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"));
		rigidbody2D.AddRelativeForce(playerVector * 1000 *  Time.deltaTime);

		if(Input.anyKey == false){
			rigidbody2D.velocity = new Vector2(Mathf.SmoothDamp(rigidbody2D.velocity.x,0, ref tempVel, smooth),Mathf.SmoothDamp(rigidbody2D.velocity.y,-1, ref tempVel, smooth));

		}

		CodeProfiler.End("Player:PlayerMovement");
	}

}
