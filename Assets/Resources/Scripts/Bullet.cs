using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {


	void Start () {
	
	}


	void Update () {
		transform.Translate(new Vector3(0,0.2f,0));
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.collider2D.name == "Player"){
			Destroy(gameObject);
			Destroy(col.gameObject);
		}
	}

}
