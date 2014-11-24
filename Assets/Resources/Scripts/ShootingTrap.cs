using UnityEngine;
using System.Collections;

public class ShootingTrap : MonoBehaviour {

	// Use this for initialization
	private float timer;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += 1f * Time.deltaTime;

		if(timer > 5){
			GameObject Bullet = Instantiate(Resources.Load("Prefabs/Bullet"), new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity) as GameObject;
			timer = 0;
		}
	}
}
