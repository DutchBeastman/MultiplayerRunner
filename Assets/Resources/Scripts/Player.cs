using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Vector2 playerVelocity;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		PlayerMovement();
	}

	void PlayerMovement()
	{

		playerVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"));
		rigidbody2D.AddRelativeForce(playerVelocity * 100);
	}
}
