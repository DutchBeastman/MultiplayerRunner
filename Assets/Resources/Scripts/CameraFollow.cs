using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public GameObject cameraTarget; // object to look at or follow
	public GameObject player; // player object for moving
	
	public float smoothTime = 1f; // time for dampen
	public bool cameraFollowX = true; // camera follows on horizontal
	public bool cameraFollowY = true; // camera follows on vertical
	public bool cameraFollowHeight = true; // camera follow CameraTarget object height
	public float cameraHeight = 2.5f; // height of camera adjustable
	public Vector2 velocity; // speed of camera movement
	
	private Transform cameraPosition; // camera Transform
	
	// Use this for initialization
	void Start () {
		cameraPosition = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (cameraFollowX){
			float tempX = Mathf.SmoothDamp (cameraPosition.position.x, cameraTarget.transform.position.x, ref velocity.x, smoothTime);
			//Doe cameraPosition.position.x in een variable.
			cameraPosition.position.x = new Vector3 (tempX , cameraPosition.position.y, cameraPosition.position.z);
		}
		if (cameraFollowY) {
			cameraPosition.position.y = new Vector3 (cameraPosition.position.x,Mathf.SmoothDamp (cameraPosition.position.y, cameraTarget.transform.position.y, ref velocity.y, smoothTime), cameraPosition.position.z);
		}
		if (!cameraFollowX & cameraFollowHeight) {
			//To Do
		}
	}
}