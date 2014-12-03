
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float smooth = 1000f;
	private Vector2 playerVector;
	private float tempVel;
	public float playerPosX;
	public float playerPosY;

	// Update is called once per frame
	void FixedUpdate () 
	{
		CodeProfiler.Begin("Player:FixedUpdate");
		playerPosX = transform.position.x;
		playerPosY = transform.position.y;
		if (networkView.isMine)
		{
			PlayerMovement();
		}
		PlayerMovement();
		CodeProfiler.End("Player:FixedUpdate");
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
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
		/*float networkWritePlayerX = 0;
		float networkWritePlayerY = 0;
		if (stream.isWriting) {
			networkWritePlayerX = playerPosX;
			networkWritePlayerY = playerPosY;
			stream.Serialize(ref networkWritePlayerX);
			stream.Serialize(ref networkWritePlayerY);


		} else {
			stream.Serialize(ref networkWritePlayerX);
			stream.Serialize(ref networkWritePlayerY);
			playerPosX = networkWritePlayerX;
			playerPosY = networkWritePlayerY;
		}*/

		//het error'd op Vector2D
		Vector3 syncPosition = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = rigidbody.position;
			stream.Serialize(ref syncPosition);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			rigidbody.position = syncPosition;
		}
	}
}
