using UnityEngine;
using System.Collections;

public class TrapControl : MonoBehaviour {

	Vector3 shootingTrapPosition;
	GameObject CurrentTrap;
	bool    MovingTrap;
	public Camera cam;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(MovingTrap);
		Ray ray = cam.ScreenPointToRay (Input.mousePosition);

		if(Input.GetMouseButtonDown(0))
		{

			MovingTrap = true;
			Debug.Log(Input.mousePosition);
			GameObject ShootingTrap = Instantiate(Resources.Load("Prefabs/ShootingTrap"), new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z), Quaternion.identity) as GameObject;
			CurrentTrap = ShootingTrap;
			if(MovingTrap == true){
				shootingTrapPosition = ray.origin;
				shootingTrapPosition.z = ray.origin.z + 9.7f;
				ShootingTrap.transform.localPosition = shootingTrapPosition;
			
				Debug.Log(ray.origin);
			}



		}
		if(Input.GetMouseButtonUp(0)){
			MovingTrap = false;

		}	                 

		if(MovingTrap == true){

			shootingTrapPosition = ray.origin;
			shootingTrapPosition.z = ray.origin.z + 9.7f;
			CurrentTrap.transform.localPosition= shootingTrapPosition;
			
			Debug.Log(ray.origin);
	}
	}
}
